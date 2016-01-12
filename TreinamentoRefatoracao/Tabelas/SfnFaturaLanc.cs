using System;
using System.Collections.Generic;

namespace TreinamentoRefatoracao.Tabelas
{
    public sealed class SfnFaturaLanc : Entidade
    {
        public SfnFaturaLanc()
        {
            Handle = Repositorio.Tabelas.RepositorioLancamento().NovoHandle();
        }
        public int HandleFatura { get; set; }
        public int TipoLancamentoFinanceiro { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public List<SfnFaturaLancCc> LancamentoCc { get; set; }
    }
}
