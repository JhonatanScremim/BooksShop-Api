using BooksShop.Catalog.Domain;

namespace BooksShop.Catalog.Repository.Interfaces
{
    public interface IAuthorRepository
    {
         Task<Author?> GetByIdAsync(int id);
    }
}