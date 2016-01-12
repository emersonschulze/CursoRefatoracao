using System.Linq;

namespace TreinamentoRefatoracao.Pesquisas
{
    public class Fatura
    {
        public Tabelas.SfnFatura PesquisarFatura(int handleFatura)
        {
            var repFatura = Repositorio.Tabelas.RepositorioFatura();
            var repLancamento = Repositorio.Tabelas.RepositorioLancamento();
            var repLancamentoCc = Repositorio.Tabelas.RepositorioLancamentoCc();


            var fatura = repFatura.Pesquisar(x => x.Handle == handleFatura).FirstOrDefault();
            if (fatura == null)
                return null;
            fatura.Lancamentos = repLancamento.Pesquisar(x => x.HandleFatura == handleFatura).ToList();

            fatura.Lancamentos.ForEach(
                lanc => lanc.LancamentoCc = repLancamentoCc.Pesquisar(x => x.HandleLancamento == lanc.Handle).ToList());
            return fatura;
        }
    }
}
