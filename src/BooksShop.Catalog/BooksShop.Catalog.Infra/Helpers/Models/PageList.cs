namespace BooksShop.Catalog.Infra.Helpers.Models
{
    public class PageList<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize  { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }

        public PageList(int currentPage, int pageSize, int totalCount, List<T> items)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }   

        public static IQueryable<T> GetPaginated(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}