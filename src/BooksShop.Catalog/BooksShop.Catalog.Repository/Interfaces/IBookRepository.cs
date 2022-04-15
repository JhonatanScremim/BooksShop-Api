using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Infra.Helpers.Models;

namespace BooksShop.Catalog.Repository.Interfaces
{
    public interface IBookRepository
    {
        IQueryable<Book>? GetPaginated(PageParams pageParams, out int totalCount);
        Task<List<Book>?> GetAllAsync();
        Task<Book?> GetByIdAsync(int? id);
         Task<Book?> GetByTitleAsync(string title);
    }
}