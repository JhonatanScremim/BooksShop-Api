using BooksShop.Catalog.Domain;

namespace BooksShop.Catalog.Repository.Interfaces
{
    public interface IBookRepository
    {
         Task<Book?> GetByTitleAsync(string title);
    }
}