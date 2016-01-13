using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinamentoRefatoracao.Faturas;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.Irrf
{
    public class IrrfFisica : IIrrf
    {
        private const int VALOR_MAXIMO_DE_IMPOSTO_DE_RENDA = 1300;
        private const decimal IRRF_15_PER_CENT = (decimal)0.15;
        private const decimal IRRF_27_PER_CENT = (decimal)0.27;
        private readonly SamBeneficiario beneficiario;

        public IrrfFisica(SamBeneficiario beneficiario)
        {
            this.beneficiario = beneficiario;
            if (beneficiario.TipoPessoa != TipoPessoaEnum.Fisica)
            throw new Exception("Beneficiário não é uma pessoa física");
        }

        public decimal CalcularIrrf(decimal valorTotalFatura)
        {
            if (valorTotalFatura > VALOR_MAXIMO_DE_IMPOSTO_DE_RENDA)
                return valorTotalFatura * IRRF_27_PER_CENT;

            return valorTotalFatura * IRRF_15_PER_CENT;
        }
       
    }
}
