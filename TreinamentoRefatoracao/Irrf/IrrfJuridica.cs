using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinamentoRefatoracao.Faturas;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.Irrf
{
    public class IrrfJuridica : IIrrf
    {
        private readonly SamBeneficiario beneficiario;

        public IrrfJuridica(SamBeneficiario beneficiario)
        {
            this.beneficiario = beneficiario;
            if (beneficiario.TipoPessoa != TipoPessoaEnum.Juridica)
            throw new Exception("Beneficiário não é uma pessoa juridica");
        }

        public decimal CalcularIrrf(decimal valorTotalFatura)
        {
            return 0;
        }

    }
    
}
