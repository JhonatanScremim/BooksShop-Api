using BooksShop.Catalog.Repository.Context;
using BooksShop.Catalog.Repository.Interfaces;

namespace BooksShop.Catalog.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public void Create<T>(T entity)
        {
            if(entity == null)
                return;
            
            _context.Add(entity);
        }

        public void Update<T>(T entity)
        {
            if(entity == null)
                return;
            
            _context.Update(entity);
        }

        public void Delete<T>(T entity)
        {
            if(entity == null)
                return;
            
            _context.Remove(entity);
        }

        public void DeleteRange<T>(List<T> entities)
        {
            _context.RemoveRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            //Caso houver alguma alteração retorna true
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}