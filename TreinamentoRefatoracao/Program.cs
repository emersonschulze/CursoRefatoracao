using System;
using TreinamentoRefatoracao.CentroDeCusto;
using TreinamentoRefatoracao.Faturas;
using TreinamentoRefatoracao.Procedures;
using TreinamentoRefatoracao.Tabelas;


namespace TreinamentoRefatoracao
{
    class Program
    {
        static void Main(string[] args)
        {
            var sfnFatura = new SfnFatura(TipoFaturaEnum.Mensalidade, new SamBeneficiario());
            var fatura = new Faturas.Fatura(new VerificaBeneficiarioInadimplente(), new CentroDeCustoRevisaoMensalidade(), sfnFatura);

            var lancamento = new Dto.Lancamento(110, 100);
            fatura.InserirLancamento(lancamento);
            fatura.Gerar();

            Console.ReadKey();

        }
    }
}
