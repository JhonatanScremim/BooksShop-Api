using BooksShop.Catalog.Infra.ViewModels;

namespace BooksShop.Catalog.External.Interfaces
{
    public interface IDiscountApiService
    {
         Task<List<BookViewModel>> GetDiscountsAsync(List<BookViewModel> books);
    }
}