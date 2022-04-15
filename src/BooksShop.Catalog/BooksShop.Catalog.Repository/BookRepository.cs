using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Infra.Helpers.Models;
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

        public IQueryable<Book>? GetPaginated(PageParams pageParams, out int totalCount)
        {
            if (_context.Book == null)
                throw new ArgumentException("Context is null");

            var query = _context.Book.Include(x => x.Publisher)
                                .Include(x => x.AuthorBooks).ThenInclude(x => x.Author).AsNoTracking();

            totalCount = query.Count();
            return PageList<Book>.GetPaginated(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<List<Book>?> GetAllAsync()
        {
            if(_context.Book == null)
                throw new ArgumentException("Context is null");

            return await _context.Book.Include(x => x.Publisher)
                                .Include(x => x.AuthorBooks).ThenInclude(x => x.Author).AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int? id)
        {
            if(id == null || _context.Book == null)
                throw new ArgumentException("Context is null");

            return await _context.Book.Include(x => x.Publisher)
                                .Include(x => x.AuthorBooks).ThenInclude(x => x.Author).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book?> GetByTitleAsync(string title)
        {
            if(title == null || _context.Book == null)
                throw new ArgumentException("Context is null");

            return await _context.Book.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}