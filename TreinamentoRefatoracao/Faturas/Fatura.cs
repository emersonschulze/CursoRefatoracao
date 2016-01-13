using System;
using System.Collections.Generic;
using System.Linq;
using TreinamentoRefatoracao.CentroDeCusto;
using TreinamentoRefatoracao.Irrf;
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
        private readonly SfnFatura sfnFatura;

        public IRepositorio<SfnFatura> repFatura = Repositorio.Tabelas.RepositorioFatura();
        public IRepositorio<SfnFaturaLanc> repLancamento = Repositorio.Tabelas.RepositorioLancamento();
        private readonly IVerificarBeneficiarioInadimplente verificaBeneficiarioInadimplente;
        private readonly AbstractCentroDeCusto centroDeCusto;
        

        /// <summary>
        /// Mudar o construtor para aceitar o tipo da fatura e o beneficiario inteiro
        /// </summary>
        public Fatura(
            IVerificarBeneficiarioInadimplente verificaBeneficiarioInadimplente,
            AbstractCentroDeCusto centroDeCusto,
            SfnFatura sfnFatura)
        {
            this.verificaBeneficiarioInadimplente = verificaBeneficiarioInadimplente;
            this.sfnFatura = sfnFatura;
            this.centroDeCusto = centroDeCusto;
            repFatura.Incluir(sfnFatura);
        }

        public void InserirLancamento(Dto.Lancamento lancamento)
        {

            if (sfnFatura.Lancamentos == null)
                sfnFatura.Lancamentos = new List<SfnFaturaLanc>();
            sfnFatura.Lancamentos.Add(new SfnFaturaLanc()
            {
                HandleFatura = sfnFatura.Handle,
                Data = DateTime.Today,
                TipoLancamentoFinanceiro = lancamento.TipoLancamentoFinanciero,
                Valor = lancamento.Valor
            });
            repLancamento.Incluir(sfnFatura.Lancamentos.Last());

        }

        public SfnFatura Gerar()
        {
            if (verificaBeneficiarioInadimplente.BeneficiarioInadimplente(sfnFatura.Beneficiario.Handle))
                throw new Exception("Beneficiário esta inadimplente");

            centroDeCusto.GerarCentroDeCusto(sfnFatura);

            sfnFatura.Valor = sfnFatura.Lancamentos.Sum(x => x.Valor);
            sfnFatura.Numero = sfnFatura.Handle;

            sfnFatura.ValorIrrf = CalculoImpostoIrrf.ObterImportoRenda(sfnFatura.Beneficiario).CalcularIrrf(sfnFatura.Valor);

            repFatura.Alterar(sfnFatura);

            return (new Pesquisas.Fatura()).PesquisarFatura(sfnFatura.Handle);
        }
    }
}
