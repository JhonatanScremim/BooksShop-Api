using BooksShop.Catalog.Application.DTOs;

namespace BooksShop.Catalog.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO?> CreateAsync(BookDTO model);
    }
}