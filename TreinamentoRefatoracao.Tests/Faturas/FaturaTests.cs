using System.Linq;
using NUnit.Framework;
using TreinamentoRefatoracao.CentroDeCusto;
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
        private static readonly Lancamento LancamentoFisicoMaior1300 = new Lancamento(1, 10000);
        private static readonly Lancamento LancamentoJuridico = new Lancamento(2, 10000);
        private AbstractCentroDeCusto centroDeCustoMensalidade;
        private AbstractCentroDeCusto centroDeCustoISS;

        private static readonly SamBeneficiario BeneficiarioFisico = new SamBeneficiario
        {
            TipoPessoa = TipoPessoaEnum.Fisica
        };

        private static readonly SamBeneficiario BeneficiarioJuridico = new SamBeneficiario
        {
            TipoPessoa = TipoPessoaEnum.Juridica
        };

        [SetUp]
        public void Setup()
        {
            centroDeCustoMensalidade = new CentroDeCustoRevisaoMensalidade();
            centroDeCustoISS = new CentroDeCustoRevisaoISS();
        }

        [Test]
        public void Deve_criar_lancamentos_de_centro_de_custo_ao_criar_uma_fatura_de_mensalidade_com_lancamento()
        {
            var criadorFatura = ObterFaturaFake(new SfnFatura(TipoFaturaEnum.Mensalidade, BeneficiarioFisico));

            criadorFatura.InserirLancamento(LancamentoFake); 

            var fatura = criadorFatura.Gerar();

            Assert.IsNotNull(fatura.Lancamentos.FirstOrDefault().LancamentoCc);
        }


        [Test]
        public void Deve_criar_lancamentos_de_centro_de_custo_ao_criar_uma_fatura_de_revisao_de_iss_com_lancamento()
        {
            var criadorFatura = ObterFaturaFake(new SfnFatura(TipoFaturaEnum.RevisaoIss, BeneficiarioFisico));

            criadorFatura.InserirLancamento(LancamentoFake);

            var fatura = criadorFatura.Gerar();

            Assert.IsNotNull(fatura.Lancamentos.FirstOrDefault().LancamentoCc);
        }

        [Test]
        public void Deve_criar_lancamentos_de_centro_de_custo_ao_criar_uma_fatura_de_revisao_de_inss_com_lancamento()
        {
            var criadorFatura = ObterFaturaFake(new SfnFatura(TipoFaturaEnum.RevisaoInss, BeneficiarioFisico));

            criadorFatura.InserirLancamento(LancamentoFake);

            var fatura = criadorFatura.Gerar();

            Assert.IsNotNull(fatura.Lancamentos.FirstOrDefault().LancamentoCc);
        }


        [Test]
        public void Deve_gerar_fatura_com_27_porcento_de_irff_para_pessoa_fisica()
        {
            var criadorFatura = ObterFaturaFake(new SfnFatura(TipoFaturaEnum.Mensalidade, BeneficiarioFisico));

            criadorFatura.InserirLancamento(LancamentoFisicoMaior1300);

            var fatura = criadorFatura.Gerar();

            Assert.AreEqual(10000 * ((decimal)0.27), fatura.ValorIrrf, "O valor da fatura deve ser de 27%");
        }

        [Test]
        public void Deve_gerar_fatura_com_0_porcento_de_irff_para_pessoa_juridica()
        {
            var criadorFatura = ObterFaturaFake(new SfnFatura(TipoFaturaEnum.Mensalidade, BeneficiarioJuridico));

            criadorFatura.InserirLancamento(LancamentoJuridico);

            var fatura = criadorFatura.Gerar();

            Assert.AreEqual(10000*0, fatura.ValorIrrf);
        }

        private static Fatura ObterFaturaFake(SfnFatura fatura)
        {
            return new Fatura(new VerificaBeneficiarioInadimplenteMock(),new CentroDeCustoMock(), fatura);
        }
    }
}