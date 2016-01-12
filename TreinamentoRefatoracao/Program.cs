using System;


namespace TreinamentoRefatoracao
{
    class Program
    {
        static void Main(string[] args)
        {

            var fatura = new Faturas.Fatura();
            fatura.SfnFatura.TipoFatura = 110;
            fatura.SfnFatura.Beneficiario = new Tabelas.SamBeneficiario();


            var lancamento = new Dto.Lancamento();
            lancamento.TipoLancamentoFinanciero = 110;
            lancamento.Valor = 100;
            fatura.InserirLancamento(lancamento);
            fatura.Executar();

            Console.ReadKey();

        }
    }
}
