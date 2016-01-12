using System;
using System.Collections.Generic;

namespace TreinamentoRefatoracao.Tabelas
{
    public sealed class SfnFatura : Entidade
    {
        public SfnFatura()
        {
            Handle = Repositorio.Tabelas.RepositorioLancamento().NovoHandle();
        }

        public int TipoFatura { get; set; }
        public int Numero { get; set; }
        public SamBeneficiario Beneficiario { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorIrrf { get; set; }
        public List<SfnFaturaLanc> Lancamentos { get; set; }
    }
}
