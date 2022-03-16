using BooksShop.Catalog.Domain;

namespace BooksShop.Catalog.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>?> GetAllAsync();
        Task<Book?> GetByIdAsync(int? id);
         Task<Book?> GetByTitleAsync(string title);
    }
}