using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PalabrasAleatoriasOrigenDeDatos.Iterators.QueryableLazyFetch
{
    public class IQueryableLazyFetchEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        public int PageSize { get; set; }
        private IQueryable<T> iQueryable;
        private int currentIndex = -1;
        private int maxIndex = 0;
        private int currentPageIndex = -1;
        private int pageIndex = 0;
        private IEnumerable<T> currentPage;

        public IQueryableLazyFetchEnumerator(IQueryable<T> items)
        {
            PageSize = 20;
            iQueryable = items;
            maxIndex = iQueryable.Count();
            currentPage = iQueryable.Skip(pageIndex * PageSize).Take(PageSize).ToArray();
        }
        public IQueryableLazyFetchEnumerator(IQueryable<T> items, int pageSize): this(items)
        {
            PageSize = pageSize;
        }


        public bool MoveNext()
        {
            currentIndex++;
            currentPageIndex++;
            var canMoveNext = currentIndex < maxIndex;
            if (canMoveNext)
            {
                var needLoadNewPage = currentPageIndex == this.PageSize;
                if (needLoadNewPage)
                {
                    currentPageIndex = 0;
                    pageIndex++;
                    currentPage = iQueryable.Skip(pageIndex * PageSize).Take(PageSize).ToArray();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            currentIndex = -1;
            currentPageIndex = -1;
            pageIndex = 0;
            currentPage = iQueryable.Skip(pageIndex * PageSize).Take(PageSize).ToArray();
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return currentPage.ElementAt(currentPageIndex);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
            iQueryable = null;
            currentIndex = -1;
            maxIndex = 0;
            currentPage = null;
            currentPageIndex = -1;
            pageIndex = 0;
        }
    }

    public class QueryableLazyFetch<T> : IQueryableLazyFetch<T>
    {
        private readonly IQueryable<T> items;

        public QueryableLazyFetch(IQueryable<T> items)
        {
            this.items = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new IQueryableLazyFetchEnumerator<T>(items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new IQueryableLazyFetchEnumerator<T>(items);
        }
    }
}
