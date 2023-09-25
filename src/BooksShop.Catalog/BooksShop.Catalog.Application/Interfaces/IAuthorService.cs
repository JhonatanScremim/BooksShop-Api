using BooksShop.Catalog.Domain;
using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.ViewModels;

namespace BooksShop.Catalog.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorViewModel?> CreateAsync(AuthorDTO model);
    }
}