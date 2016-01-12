using System;
using System.Linq;
using System.Linq.Expressions;

namespace TreinamentoRefatoracao.Repositorio
{
    public interface IRepositorio<T>
    {
        
        bool EhRepositorioTeste { get; }


        // INSERT
        T Incluir(T entity);

        // UPDATE
        T Alterar(T entity);

        // DELETE
        void Excluir(T entity);


        // Retorna Todas as Entidades
        IQueryable<T> RetornarTodos();

        // Consulta Todas as Entidades de acordo com condição where
        IQueryable<T> Pesquisar(Expression<Func<T, bool>> where);

        // Permite Expressão LINQ
        IQueryable<T> Pesquisar();

        Int32 NovoHandle();

    }
}
