using System;
using NUnit.Framework;
using TreinamentoRefatoracao.Faturas;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.Tests.Tabelas
{
    [TestFixture]
    public class SfnFaturaTests
    {
        [Test]
        [ExpectedException(typeof(Exception))]
        public void Nao_deve_ser_possivel_instanciar_com_beneficiario_invalido()
        {
            
        }

    }
}