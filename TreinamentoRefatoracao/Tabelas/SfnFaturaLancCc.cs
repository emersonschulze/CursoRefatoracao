namespace TreinamentoRefatoracao.Tabelas
{
    public sealed class SfnFaturaLancCc : Entidade
    {
        public SfnFaturaLancCc()
        {
            Handle = Repositorio.Tabelas.RepositorioLancamento().NovoHandle();
        }

        public int HandleLancamento { get; set; }
        public string CentroDeCusto { get; set; }
        public decimal Valor { get; set; }

    }
}
