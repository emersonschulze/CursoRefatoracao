using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinamentoRefatoracao.Repositorio;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.CentroDeCusto
{
    public class CentroDeCustoRevisaoISS : AbstractCentroDeCusto
    {
        
        protected override void GerarCentroCustoEspecifico(SfnFatura fatura)
        {
            foreach (var lancamentoCentroCusto in fatura.Lancamentos)
            {
                if (lancamentoCentroCusto.LancamentoCc == null)
                    lancamentoCentroCusto.LancamentoCc = new List<SfnFaturaLancCc>();
                lancamentoCentroCusto.LancamentoCc.Add(CriarLancCc("Mensalidade", lancamentoCentroCusto.Handle, lancamentoCentroCusto.Valor / 2));
                repLancamentoCc.Incluir(lancamentoCentroCusto.LancamentoCc.Last());

                var valorLancamento = Math.Round(lancamentoCentroCusto.Valor / 2, 2);
                lancamentoCentroCusto.LancamentoCc.Add(CriarLancCc("Mensalidade", lancamentoCentroCusto.HandleFatura,
                    valorLancamento));
                repLancamentoCc.Incluir(lancamentoCentroCusto.LancamentoCc.Last());
            }
        }
    }
}
