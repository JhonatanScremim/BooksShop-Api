using BooksShop.Basket.Domain;

namespace BooksShop.Basket.Services.Interfaces
{
    public interface IBasketService
    {
         Task<ShoppingCart?> GetBasketAsync(string userName);
         Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket);
         Task DeleteBasket(string userName);
    }
}