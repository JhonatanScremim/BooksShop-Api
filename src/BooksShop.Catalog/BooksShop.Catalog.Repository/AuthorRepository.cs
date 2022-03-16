using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Repository.Context;
using BooksShop.Catalog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksShop.Catalog.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            if(id == default || _context.Author == null)
                return null;

            return await _context.Author.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Author>?> GetByListIdsAsync(List<int>? ids)
        {
            if(ids == null || _context.Author == null)
                return null;
            
            var authors = new List<Author>();

            foreach(var item in ids)
            {    
                var authorFinder = await _context.Author.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item);

                if(authorFinder != null)
                    authors.Add(authorFinder);
            }

            return authors;
        }
    }
}