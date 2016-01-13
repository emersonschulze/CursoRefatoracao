namespace TreinamentoRefatoracao.Dto
{
    public class Lancamento
    {
        public Lancamento(int tipoLancamentoFinanciero, decimal valor)
        {
            TipoLancamentoFinanciero = tipoLancamentoFinanciero;
            Valor = valor;
        }

        public int TipoLancamentoFinanciero { get; set; }
        public decimal Valor { get; set; }
    }
}
