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
        public SfnFatura SfnFatura { get; set; }


        public IRepositorio<SfnFatura> repFatura = Repositorio.Tabelas.RepositorioFatura();
        public IRepositorio<SfnFaturaLanc> repLancamento = Repositorio.Tabelas.RepositorioLancamento();
        public IRepositorio<SfnFaturaLancCc> repLancamentoCc = Repositorio.Tabelas.RepositorioLancamentoCc();

        /// <summary>
        /// Mudar o construtor para aceitar o tipo da fatura e o beneficiario inteiro
        /// </summary>
        public Fatura()
        {
            SfnFatura = new SfnFatura();

            repFatura.Incluir(SfnFatura);
        }

        public void InserirLancamento(Dto.Lancamento l)
        {

            if (SfnFatura.Lancamentos == null)
                SfnFatura.Lancamentos = new List<SfnFaturaLanc>();
            SfnFatura.Lancamentos.Add(new SfnFaturaLanc()
            {
                HandleFatura = SfnFatura.Handle,
                Data = DateTime.Today,
                TipoLancamentoFinanceiro = l.TipoLancamentoFinanciero,
                Valor = l.Valor
            });
            repLancamento.Incluir(SfnFatura.Lancamentos.Last());

        }

        private void GerarCentroDecusto()
        {
            if (SfnFatura.TipoFatura == 120) // Fatura de Mensalidade
                GerarCentroCustoMensalidade();
            else
                if (SfnFatura.TipoFatura == 620) // Fatura de Revisão de ISS
                    GerarCentroCustoRevisaoIss();
                else
                    throw new Exception("Tipo de fatura não reconhecido");
        }

        public void GerarCentroCustoRevisaoIss()
        {
            foreach (var l in SfnFatura.Lancamentos)
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
            SfnFatura.Lancamentos.ForEach(lanc =>
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

        public void Calcular()
        {
            if (SfnFatura.Beneficiario.TipoPessoa == 1)
            {
                if (SfnFatura.Valor > 1300)
                    SfnFatura.ValorIrrf = SfnFatura.Valor * (decimal)0.27;
                else
                    SfnFatura.ValorIrrf = SfnFatura.Valor * (decimal)0.15;
            }
            if (SfnFatura.Beneficiario.TipoPessoa == 2)
                SfnFatura.ValorIrrf = 0;
        }

        public SfnFatura Executar()
        {
            var inad = new VerificaBeneficiarioInadimplente();
            if (inad.BeneficiarioInadimplente(SfnFatura.Beneficiario.Handle))
                throw new Exception("Beneficiário esta inadimplente");

            GerarCentroDecusto();

            SfnFatura.Valor = SfnFatura.Lancamentos.Sum(x => x.Valor);
            SfnFatura.Numero = SfnFatura.Handle;

            Calcular();

            repFatura.Alterar(SfnFatura);

            return (new Pesquisas.Fatura()).PesquisarFatura(SfnFatura.Handle);
        }
    }
}
