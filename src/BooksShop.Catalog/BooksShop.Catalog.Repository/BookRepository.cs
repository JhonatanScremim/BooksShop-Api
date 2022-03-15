using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Repository.Context;
using BooksShop.Catalog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksShop.Catalog.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Book>?> GetAllAsync()
        {
            if(_context.Book == null)
                return null;

            return await _context.Book.Include(x => x.Publisher)
                                .Include(x => x.AuthorBooks).ThenInclude(x => x.Author).AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetByTitleAsync(string title)
        {
            if(title == null || _context.Book == null)
                return null;

            return await _context.Book.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}