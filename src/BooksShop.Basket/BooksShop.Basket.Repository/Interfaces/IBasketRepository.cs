using BooksShop.Basket.Domain;

namespace BooksShop.Basket.Repository.Interfaces
{
    public interface IBasketRepository
    {
         Task<ShoppingCart?> GetBasketAsync(string userName);
         Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket);
         Task DeleteBasket(string userName);
    }
}