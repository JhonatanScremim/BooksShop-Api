using System.ComponentModel.DataAnnotations;

namespace BooksShop.Catalog.Infra.Helpers.Models
{
    public class PageParams
    {
        [Range(1, 50)]
        public int PageNumber { get; set; }
        [Range(1, 50)]
        public int PageSize { get; set; }
    }
}