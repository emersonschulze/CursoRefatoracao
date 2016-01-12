using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TreinamentoRefatoracao.Tabelas;

namespace TreinamentoRefatoracao.Repositorio
{
    /// <summary>
    /// Classe responsavel por implementar o repositorio de teste
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repositorio<T> : IRepositorio<T> where T : Entidade
    {
        private readonly List<T> lista = new List<T>();

        /// <summary>
        /// Instancia um repositorio de teste
        /// </summary>
        /// <param name="carregarRepositorio">
        /// Indica se deve ser carregado o repositório em XML. 
        /// Por padrão, o repositório deverá carregado no teste unitário
        /// </param>
        public Repositorio(bool carregarRepositorio = false, Expression<Func<T, bool>> condicao = null)
        {
            if (carregarRepositorio)
            {
                if (condicao != null)
                    lista = Pesquisar(condicao).ToList();
            }
        }

        public T Incluir(T entity)
        {
            if (entity.Handle <= 0)
                entity.Handle = NovoHandle();

            lista.Add(entity.Clonar<T>());

            return entity;
        }


        public T Alterar(T entity)
        {
            var ent = lista.FirstOrDefault(x => x.Handle == entity.Handle);
            T newEntity;

            if (ent != null)
            {
                Excluir(entity);
                newEntity = entity.Clonar<T>();
                Incluir(newEntity);
            }
            else
            {
                newEntity = entity;
            }

            return newEntity;
        }


        public void Excluir(T entity)
        {
            lista.Remove(entity);
        }

        public IQueryable<T> RetornarTodos()
        {
            return Pesquisar();
        }

        public IQueryable<T> Pesquisar(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return (lista.Select(item => item.Clonar<T>()).ToList())
                .Select(x => x).AsQueryable().Where(where);
        }

        public IQueryable<T> Pesquisar()
        {
            return (lista.Select(item => item.Clonar<T>()).ToList())
                .Select(x => x).AsQueryable();
        }


        public Int32 NovoHandle()
        {
            if (lista.Any())
                return lista.Max(t => t.Handle) + 1;

            return 1;
        }

        /// <summary>
        /// para indicar se o repositorio é o de teste
        /// </summary>
        public bool EhRepositorioTeste
        {
            get { return true; }
        }

    }
}

