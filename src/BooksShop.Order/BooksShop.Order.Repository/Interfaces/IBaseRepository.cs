using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksShop.Order.Repository.Interfaces
{
    public interface IBaseRepository
    {
        void Create<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);
        void DeleteRange<T>(List<T> entities);
        Task<bool> SaveChangesAsync();
    }
}