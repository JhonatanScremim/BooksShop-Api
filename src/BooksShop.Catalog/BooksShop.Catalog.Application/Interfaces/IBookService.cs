using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.Helpers.Models;
using BooksShop.Catalog.Infra.ViewModels;

namespace BooksShop.Catalog.Application.Interfaces
{
    public interface IBookService
    {
        PageList<BookViewModel>? GetPaginated(PageParams pageParams);
        Task<List<BookViewModel>?> GetAllAsync();
        Task<BookViewModel?> CreateAsync(BookDTO model);
        Task<BookViewModel?> UpdateAsync(int bookId, BookDTO model);
        Task<bool> Delete(int? bookId);
    }
}