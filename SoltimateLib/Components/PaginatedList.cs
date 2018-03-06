using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SoltimateLib.Components
{
    /// <summary>
    /// Class for paginating results.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Currently selected page.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Total amount of pages.
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Create new instance of PaginatedList. If created with constructor, you have to limit the items of page manually.
        /// </summary>
        /// <param name="items">Items on the page.</param>
        /// <param name="count">Total count of items.</param>
        /// <param name="pageIndex">Currently selected page.</param>
        /// <param name="pageSize">Pagesize.</param>
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        /// <summary>
        /// Tell if there is previous page or not of the whole set.
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// Tell if the page is last page of the whole set.
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        /// <summary>
        /// Create new instance of PaginatedList from IQueryable<T> source.
        /// </summary>
        /// <param name="source">The source of data.</param>
        /// <param name="pageIndex">The selected page.</param>
        /// <param name="pageSize">Size of each page.</param>
        /// <returns>New instance of PaginatedList with the data.</returns>
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
