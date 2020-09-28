using System.Collections.Generic;

namespace PDFKeyValuesHelper
{
    public interface IPDFKeyValuesHelper<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CountPages { get; }
        public IEnumerable<T> GetItemsForPage(int pageNumber);
        public IEnumerable<T> GetItemsForLastPage();
    }
}
