using System;
using TreinamentoRefatoracao.Faturas;

namespace TreinamentoRefatoracao.Tabelas
{
    public class SamBeneficiario
    {
        public int Handle { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public TipoPessoaEnum TipoPessoa { get; set; }
    }
}
