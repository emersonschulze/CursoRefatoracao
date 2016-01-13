using System.Linq;
using NUnit.Framework;
using TreinamentoRefatoracao.Dto;
using TreinamentoRefatoracao.Faturas;
using TreinamentoRefatoracao.Tabelas;
using TreinamentoRefatoracao.Tests.Mocks;

namespace TreinamentoRefatoracao.Tests.Faturas
{
    [TestFixture]
    public class FaturaTests
    {
        private static readonly Lancamento LancamentoFake = new Lancamento(1, 100);

        private static readonly SamBeneficiario BeneficiarioFisico = new SamBeneficiario
        {
            TipoPessoa = TipoPessoa.Fisica
        };

        [Test]
        public void Deve_criar_lancamentos_de_centro_de_custo_ao_criar_uma_fatura_de_mensalidade_com_lancamento()
        {
            var criadorFatura = ObterFaturaFake(new SfnFatura(TipoFatura.Mensalidade, BeneficiarioFisico));

            criadorFatura.InserirLancamento(LancamentoFake); 

            var fatura = criadorFatura.Gerar();

            Assert.IsNotNull(fatura.Lancamentos.FirstOrDefault().LancamentoCc);
        }


        [Test]
        public void Deve_criar_lancamentos_de_centro_de_custo_ao_criar_uma_fatura_de_revisao_de_iss_com_lancamento()
        {
            var criadorFatura = ObterFaturaFake(new SfnFatura(TipoFatura.RevisaoIss, BeneficiarioFisico));

            criadorFatura.InserirLancamento(LancamentoFake);

            var fatura = criadorFatura.Gerar();

            Assert.IsNotNull(fatura.Lancamentos.FirstOrDefault().LancamentoCc);
        }

        private static Fatura ObterFaturaFake(SfnFatura fatura)
        {
            return new Fatura(new VerificaBeneficiarioInadimplenteMock(), fatura);
        }
    }
}