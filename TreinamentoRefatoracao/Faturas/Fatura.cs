using System;
using System.Collections.Generic;
using System.Linq;
using TreinamentoRefatoracao.Procedures;
using TreinamentoRefatoracao.Repositorio;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.Faturas
{
    /// <summary>
    /// Classe responsável por gerar faturas
    /// </summary>
    public class Fatura
    {
        private const int VALOR_MAXIMO_DE_IMPOSTO_DE_RENDA = 1300;
        private const double IRRF_15_PER_CENT = 0.15;
        private const double IRRF_27_PER_CENT = 0.27;

        private readonly SfnFatura sfnFatura;

        public IRepositorio<SfnFatura> repFatura = Repositorio.Tabelas.RepositorioFatura();
        public IRepositorio<SfnFaturaLanc> repLancamento = Repositorio.Tabelas.RepositorioLancamento();
        public IRepositorio<SfnFaturaLancCc> repLancamentoCc = Repositorio.Tabelas.RepositorioLancamentoCc();
        private readonly IVerificarBeneficiarioInadimplente verificaBeneficiarioInadimplente;

        /// <summary>
        /// Mudar o construtor para aceitar o tipo da fatura e o beneficiario inteiro
        /// </summary>
        public Fatura(
            IVerificarBeneficiarioInadimplente verificaBeneficiarioInadimplente, 
            SfnFatura sfnFatura)
        {
            this.verificaBeneficiarioInadimplente = verificaBeneficiarioInadimplente;
            this.sfnFatura = sfnFatura;
            repFatura.Incluir(sfnFatura);
        }

        public void InserirLancamento(Dto.Lancamento l)
        {

            if (sfnFatura.Lancamentos == null)
                sfnFatura.Lancamentos = new List<SfnFaturaLanc>();
            sfnFatura.Lancamentos.Add(new SfnFaturaLanc()
            {
                HandleFatura = sfnFatura.Handle,
                Data = DateTime.Today,
                TipoLancamentoFinanceiro = l.TipoLancamentoFinanciero,
                Valor = l.Valor
            });
            repLancamento.Incluir(sfnFatura.Lancamentos.Last());

        }

        private void GerarCentroDecusto()
        {
            if (sfnFatura.TipoFatura == TipoFatura.Mensalidade)
                GerarCentroCustoMensalidade();
            else
                if (sfnFatura.TipoFatura == TipoFatura.RevisaoIss)
                    GerarCentroCustoRevisaoIss();
                else
                    throw new Exception("Tipo de fatura não reconhecido");
        }

        public void GerarCentroCustoRevisaoIss()
        {
            foreach (var l in sfnFatura.Lancamentos)
            {
                if (l.LancamentoCc == null)
                    l.LancamentoCc = new List<SfnFaturaLancCc>();
                l.LancamentoCc.Add(new SfnFaturaLancCc
                {
                    CentroDeCusto = "Mensalidade",
                    HandleLancamento = l.Handle,
                    Valor = Math.Round(l.Valor / 2, 2)
                });
                repLancamentoCc.Incluir(l.LancamentoCc.Last());

                var a = Math.Round(l.Valor / 2, 2);
                l.LancamentoCc.Add(new SfnFaturaLancCc
                {
                    CentroDeCusto = "Mensalidade",
                    HandleLancamento = l.Handle,
                    Valor = a
                });
                repLancamentoCc.Incluir(l.LancamentoCc.Last());
            }
        }

        public void GerarCentroCustoMensalidade()
        {
            sfnFatura.Lancamentos.ForEach(lanc =>
            {
                if (lanc.LancamentoCc == null)
                    lanc.LancamentoCc = new List<SfnFaturaLancCc>();
                lanc.LancamentoCc.Add(new SfnFaturaLancCc
                {
                    CentroDeCusto = "Mensalidade",
                    HandleLancamento = lanc.Handle,
                    Valor = lanc.Valor
                });
                repLancamentoCc.Incluir(lanc.LancamentoCc.Last());
            });
        }

        private void CalcularIrrf()
        {
            if (sfnFatura.Beneficiario.TipoPessoa == TipoPessoa.Fisica)
            {
                if (sfnFatura.Valor > VALOR_MAXIMO_DE_IMPOSTO_DE_RENDA)
                {
                    sfnFatura.ValorIrrf = sfnFatura.Valor * (decimal) IRRF_27_PER_CENT;
                }
                else
                    sfnFatura.ValorIrrf = sfnFatura.Valor * (decimal) IRRF_15_PER_CENT;
            }
            if (sfnFatura.Beneficiario.TipoPessoa == TipoPessoa.Juridica)
                sfnFatura.ValorIrrf = 0;
        }

        public SfnFatura Gerar()
        {
            if (verificaBeneficiarioInadimplente.BeneficiarioInadimplente(sfnFatura.Beneficiario.Handle))
                throw new Exception("Beneficiário esta inadimplente");

            GerarCentroDecusto();

            sfnFatura.Valor = sfnFatura.Lancamentos.Sum(x => x.Valor);
            sfnFatura.Numero = sfnFatura.Handle;

            CalcularIrrf();

            repFatura.Alterar(sfnFatura);

            return (new Pesquisas.Fatura()).PesquisarFatura(sfnFatura.Handle);
        }
    }
}
