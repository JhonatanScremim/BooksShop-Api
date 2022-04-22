using BooksShop.Basket.Domain;
using BooksShop.Basket.Infra.Models;

namespace BooksShop.Basket.Application.Interfaces
{
    public interface IBasketService
    {
        Task<ShoppingCart?> GetBasketAsync(string userName);
        Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket);
        Task DeleteBasket(string userName);
        void Checkout(BasketCheckout basketCheckout);
    }
}