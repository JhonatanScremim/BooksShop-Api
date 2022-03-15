using BooksShop.Catalog.Application.DTOs;
using BooksShop.Catalog.Application.ViewModels;

namespace BooksShop.Catalog.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookViewModel>?> GetAllAsync();
        Task<BookViewModel?> CreateAsync(BookDTO model);
    }
}