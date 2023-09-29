using System.Runtime.Serialization.Json;
using System.Text.Json;
using BooksShop.Basket.Domain;
using BooksShop.Basket.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace BooksShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDistributedCache _redisCache;

        public BasketService(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart?> GetBasketAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if(string.IsNullOrEmpty(basket))
                return null;

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket)
        {
            if(string.IsNullOrEmpty(basket.UserName))
                return null;

            await _redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));

            return await GetBasketAsync(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}