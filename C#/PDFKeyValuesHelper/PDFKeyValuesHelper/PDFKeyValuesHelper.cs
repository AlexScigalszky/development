using System.Collections.Generic;
using System.Linq;

namespace PDFKeyValuesHelper
{
    public class PDFKeyValuesHelper<T> : IPDFKeyValuesHelper<T>
    {
        private IEnumerable<T> items;
        private int countPages;
        private int countItemsPagesWithoutLast;
        public int countItemsPerPage;
        public int countItemsForLastPage;

        public IEnumerable<T> Items
        {
            get => items;
            set
            {
                items = value;
                Initialize();
            }
        }

        public int CountPages
        {
            get => countPages;
        }

        public PDFKeyValuesHelper(IEnumerable<T> items, int countItemsPerPage, int countItemsForLastPage)
        {
            this.items = items;
            this.countItemsPerPage = countItemsPerPage;
            this.countItemsForLastPage = countItemsForLastPage;
            Initialize();
        }

        public PDFKeyValuesHelper(IEnumerable<T> items, int countItemsPerPage) : this(items, countItemsPerPage, countItemsPerPage)
        {

        }

        private void Initialize()
        {
            countPages = items.Count() / countItemsPerPage;
            countItemsPagesWithoutLast = countPages * countItemsPerPage;
            if (items.Count() % countItemsPerPage > 0 || countItemsPerPage != countItemsForLastPage)
            {
                countPages++;
            }
        }

        /// <summary>
        ///  Return the items to fill the page
        /// </summary>
        /// <param name="pageNumber">Start with 0 (zero) </param>
        /// <returns></returns>
        public IEnumerable<T> GetItemsForPage(int pageNumber)
        {
            return items
                .Skip(pageNumber * countItemsPerPage)
                .Take(countItemsPerPage);
        }

        public IEnumerable<T> GetItemsForLastPage()
        {
            return items
                .Skip(countItemsPagesWithoutLast);
        }
    }
}
