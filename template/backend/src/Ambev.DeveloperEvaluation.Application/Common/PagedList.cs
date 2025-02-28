using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    /// <summary>
    /// PagedList é uma coleção personalizada para lidar com resultados paginados.
    /// </summary>
    public class PagedList<T> where T : class
    {
        public List<T> Items { get; set; } = new List<T>(); // Lista de itens na página
        public int CurrentPage { get; set; } = 1;           // Número da página atual
        public int TotalPages { get; set; } = 1;            // Número total de páginas
        public int PageSize { get; set; } = 10;             // Tamanho de cada página
        public int TotalCount { get; set; } = 0;            // Número total de itens

        /// <summary>
        /// Método para inicializar a PagedList.
        /// </summary>
        /// <param name="items">Itens da página atual</param>
        /// <param name="totalCount">Contagem total de itens</param>
        /// <param name="currentPage">Número da página atual</param>
        /// <param name="pageSize">Número de itens por página</param>
        public PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        }

        /// <summary>
        /// Método para criar uma nova instância de PagedList a partir de um IQueryable.
        /// </summary>
        /// <param name="source">Fonte dos itens a serem paginados (IQueryable)</param>
        /// <param name="pageNumber">Número da página atual</param>
        /// <param name="pageSize">Número de itens por página</param>
        /// <returns>Uma nova instância de PagedList</returns>
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            // Obter a contagem total de itens
            var totalCount = source.Count();

            // Pular os itens das páginas anteriores e pegar os itens da página atual
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Criar a instância de PagedList com os itens e metadados
            return new PagedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
