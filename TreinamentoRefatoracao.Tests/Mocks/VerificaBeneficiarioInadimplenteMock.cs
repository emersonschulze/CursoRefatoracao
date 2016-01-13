using TreinamentoRefatoracao.Procedures;

namespace TreinamentoRefatoracao.Tests.Mocks
{
    public class VerificaBeneficiarioInadimplenteMock : IVerificarBeneficiarioInadimplente
    {
        public bool BeneficiarioInadimplente(int handleBeneficiario)
        {
            return false;
        }
    }
}