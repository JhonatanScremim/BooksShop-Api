using System.ComponentModel.DataAnnotations.Schema;
namespace BooksShop.Catalog.Repository.Interfaces
{
    public interface IBaseRepository
    {
        void Create<T>(T entity);
        Task CreateAsync<T>(T entity);
        void AddRange<T>(List<T> entities);
        void Update<T>(T entity);
        void Delete<T>(T entity);
        void DeleteRange<T>(List<T> entities);
        Task<bool> SaveChangesAsync();
        void DetachLocal<T>(T entity);
    }
}