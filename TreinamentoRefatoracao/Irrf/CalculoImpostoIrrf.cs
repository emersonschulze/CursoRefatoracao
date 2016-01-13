using TreinamentoRefatoracao.Faturas;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.Irrf
{
    public class CalculoImpostoIrrf 
    {
        public static IIrrf ObterImportoRenda(SamBeneficiario beneficiario)
        {
            if(beneficiario.TipoPessoa == TipoPessoaEnum.Fisica)
                return new IrrfFisica(beneficiario);

            return new IrrfJuridica(beneficiario);
        }
    }
}
