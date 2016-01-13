using System;
using TreinamentoRefatoracao.Faturas;
using TreinamentoRefatoracao.Procedures;
using TreinamentoRefatoracao.Tabelas;


namespace TreinamentoRefatoracao
{
    class Program
    {
        static void Main(string[] args)
        {
            var sfnFatura = new SfnFatura(TipoFatura.Mensalidade, new SamBeneficiario());
            var fatura = new Faturas.Fatura(new VerificaBeneficiarioInadimplente(), sfnFatura);

            var lancamento = new Dto.Lancamento(110, 100);
            fatura.InserirLancamento(lancamento);
            fatura.Gerar();

            Console.ReadKey();

        }
    }
}
