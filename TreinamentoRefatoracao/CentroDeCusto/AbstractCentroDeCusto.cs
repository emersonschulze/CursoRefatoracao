using System;
using TreinamentoRefatoracao.Repositorio;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.CentroDeCusto
{
    public abstract class AbstractCentroDeCusto
    {
        protected IRepositorio<SfnFaturaLancCc> repLancamentoCc = Repositorio.Tabelas.RepositorioLancamentoCc();

        public void GerarCentroDeCusto(SfnFatura fatura)
        {
            GerarCentroCustoEspecifico(fatura);
        }

        protected abstract void GerarCentroCustoEspecifico(SfnFatura fatura);

        protected SfnFaturaLancCc CriarLancCc(String centroDeCusto, int handleLancamento, decimal valorLancamento)
        {
            return new SfnFaturaLancCc
            {
                CentroDeCusto = centroDeCusto,
                HandleLancamento = handleLancamento,
                Valor = valorLancamento
            };
        }
    }
}
