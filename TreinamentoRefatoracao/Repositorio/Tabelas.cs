
namespace TreinamentoRefatoracao.Repositorio
{
    public class Tabelas
    {
        private static IRepositorio<TreinamentoRefatoracao.Tabelas.SfnFatura> repositorioFatura;
        public static IRepositorio<TreinamentoRefatoracao.Tabelas.SfnFatura> RepositorioFatura()
        {
            return repositorioFatura ??
                   (repositorioFatura = new Repositorio<TreinamentoRefatoracao.Tabelas.SfnFatura>());
        }

        private static IRepositorio<TreinamentoRefatoracao.Tabelas.SfnFaturaLanc> repositorioLancamento;
        public static IRepositorio<TreinamentoRefatoracao.Tabelas.SfnFaturaLanc> RepositorioLancamento()
        {
            return repositorioLancamento ??
                   (repositorioLancamento = new Repositorio<TreinamentoRefatoracao.Tabelas.SfnFaturaLanc>());
        }

        private static IRepositorio<TreinamentoRefatoracao.Tabelas.SfnFaturaLancCc> repositorioLancamentoCc;
        public static IRepositorio<TreinamentoRefatoracao.Tabelas.SfnFaturaLancCc> RepositorioLancamentoCc()
        {
            return repositorioLancamentoCc ??
                   (repositorioLancamentoCc = new Repositorio<TreinamentoRefatoracao.Tabelas.SfnFaturaLancCc>());
        }

    }
}
