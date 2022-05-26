using System.Data;

namespace Example.Models
{
    public class PageableDataTable
    {
        public int CountTotal { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DataTable DataTable { get; set; }

        public PageableDataTable()
        {
            DataTable = new();
        }
        public PageableDataTable(int CountTotal, int PageNumber, int PageSize, DataTable DataTable) : this()
        {
            this.CountTotal = CountTotal;
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.DataTable = DataTable;
        }

    }
}
