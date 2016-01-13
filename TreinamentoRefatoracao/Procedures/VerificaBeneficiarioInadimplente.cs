using System;

namespace TreinamentoRefatoracao.Procedures
{
    public class VerificaBeneficiarioInadimplente : IVerificarBeneficiarioInadimplente
    {
        /// <summary>
        /// Verifica se o beneficiário esta inadimplente
        /// </summary>
        /// <param name="handleBeneficiario">Handle do beneficiario</param>
        /// <returns>Se o beneficiário estiver inadimplente, retorna true, caso contrario false</returns>
        public bool BeneficiarioInadimplente(int handleBeneficiario)
        {
            throw new Exception("Acesso ao banco de dados não permitido");
        }
    }
}
