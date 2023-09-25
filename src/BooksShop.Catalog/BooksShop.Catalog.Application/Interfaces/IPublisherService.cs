using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.ViewModels;

namespace BooksShop.Catalog.Application.Interfaces
{
    public interface IPublisherService
    {
        Task<PublisherViewModel?> CreateAsync(PublisherDTO model);
    }
}