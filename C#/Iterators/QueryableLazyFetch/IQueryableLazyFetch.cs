using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PalabrasAleatoriasOrigenDeDatos.Iterators.QueryableLazyFetch
{
    public class IQueryableLazyFetchEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        public int PageSize { get; set; }
        private IQueryable<T> iQueryable;
        private int currentIndex = -1;
        private int maxIndex = 0;
        private int pageIndex = 0;
        private IEnumerable<T> currentPage;

        public IQueryableLazyFetchEnumerator(IQueryable<T> items)
        {
            PageSize = 1000;
            iQueryable = items;
            maxIndex = iQueryable.Count() - 1;
            currentPage = iQueryable.Skip(pageIndex * PageSize).Take(PageSize).ToArray();
        }

        public T Current { get; private set; } = default;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            iQueryable = null;
            currentIndex = -1;
            maxIndex = 0;
            Current = default;
            currentPage = null;
            pageIndex = 0;
        }

        public bool MoveNext()
        {
            var isFirstOne = currentIndex == -1 && this.maxIndex > 0;
            if (isFirstOne)
            {
                currentIndex++;
                Current = currentPage.ElementAt(currentIndex - pageIndex * PageSize);
                return true;
            }
            var isLastOne = currentIndex + 1 == this.maxIndex;
            if (isLastOne)
            {
                currentIndex++;
                Current = currentPage.ElementAt(currentIndex - pageIndex * PageSize);
                return true;
            }
            var isInNextPage = currentIndex + 1 == ((pageIndex + 1) * PageSize) && currentIndex < maxIndex;
            if (isInNextPage)
            {
                currentIndex++;
                pageIndex++;
                currentPage = iQueryable.Skip(pageIndex * PageSize).Take(PageSize).ToArray();
                Current = currentPage.ElementAt(currentIndex - pageIndex * PageSize);
                return true;
            }
            var isInCurrentPage = currentIndex + 1 <= ((pageIndex + 1) * PageSize) && currentIndex < maxIndex;
            if (isInCurrentPage)
            {
                currentIndex++;
                Current = currentPage.ElementAt(currentIndex - pageIndex * PageSize);
                return true;
            }
            return false;
        }

        public void Reset()
        {
            currentIndex = -1;
            pageIndex = 0;
            currentPage = iQueryable.Skip(pageIndex * PageSize).Take(PageSize).ToArray();
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
