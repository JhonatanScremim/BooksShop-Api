using BooksShop.Basket.Application.Interfaces;
using BooksShop.Basket.Domain;
using BooksShop.Basket.Infra.Interfaces;
using BooksShop.Basket.Infra.Models;
using BooksShop.Basket.Repository.Interfaces;

namespace BooksShop.Basket.Application
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public BasketService(IBasketRepository basketRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _basketRepository = basketRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        public Task<ShoppingCart?> GetBasketAsync(string userName)
        {
            return _basketRepository.GetBasketAsync(userName);
        }

        public Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket)
        {
            return _basketRepository.UpdateBasketAsync(basket);
        }
        public Task DeleteBasket(string userName)
        {
            return _basketRepository.DeleteBasket(userName);
        }

        public void Checkout(BasketCheckout basketCheckout)
        {
            _rabbitMQMessageSender.SendMessage(basketCheckout, "Checkout Queue");
        }
    }
}