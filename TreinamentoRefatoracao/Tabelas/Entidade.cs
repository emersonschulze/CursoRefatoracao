using System;
using System.ComponentModel.DataAnnotations;

namespace TreinamentoRefatoracao.Tabelas
{
    
    /// <summary>
    /// Interface base para todas as classes de mapeamento
    ///   esta interface é necessária para a escrita de metodos genericos para todas as classes.
    /// </summary>
    [Serializable]
    public class Entidade
    {
        public Entidade()
        {
            Handle = 0;
        }

        [Key]
        public virtual Int32 Handle { get; set; }

        public virtual string RowId { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entidade;
            if (other == null) return false;
            if (Handle == 0 && other.Handle == 0)
                return (object)this == other;
            return Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            if (Handle == 0) return base.GetHashCode();
            string stringRepresentation =
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName
                + "#" + Handle.ToString();
            return stringRepresentation.GetHashCode();
        }

        public virtual T Clonar<T>()
        {
            return (T)this.MemberwiseClone();
        }
    }
}
