using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinamentoRefatoracao.Repositorio;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.CentroDeCusto
{
   public class CentroDeCustoRevisaoMensalidade : AbstractCentroDeCusto
    {

       protected override void GerarCentroCustoEspecifico(SfnFatura fatura)
       {
           fatura.Lancamentos.ForEach(lanc =>
           {
               if (lanc.LancamentoCc == null)
                   lanc.LancamentoCc = new List<SfnFaturaLancCc>();
               lanc.LancamentoCc.Add(CriarLancCc("Mensalidade", lanc.Handle, lanc.Valor));
               repLancamentoCc.Incluir(lanc.LancamentoCc.Last());
           });
       }
    }
}
