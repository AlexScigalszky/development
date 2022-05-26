namespace Example.Models
{
    public class PageableList<T>
    {
        public int CountTotal { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ICollection<T> List { get; set; }

        public PageableList()
        {
            List = new List<T>();
        }
        public PageableList(int CountTotal, int PageNumber, int PageSize, ICollection<T> List) : this()
        {
            this.CountTotal = CountTotal;
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.List = List;
        }
    }
}
