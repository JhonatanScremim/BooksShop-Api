using BooksShop.Basket.Application.Interfaces;
using BooksShop.Basket.Domain;
using BooksShop.Basket.Infra;
using BooksShop.Basket.Infra.Models;
using BooksShop.Basket.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetBasketAsync(string userName)
        {
            try
            {
                return Ok(await _basketService.GetBasketAsync(userName) ??  new ShoppingCart(userName));
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasketAsync(ShoppingCart basket)
        {
            try
            {
                return Ok(await _basketService.UpdateBasketAsync(basket));
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasketAsync(string userName)
        {
            try
            {
                await _basketService.DeleteBasket(userName);
                return Ok();
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            try
            {
                Console.WriteLine("Entrou no endpoint");
                var basket = await _basketService.GetBasketAsync(basketCheckout.UserName);

                if (basket == null)
                    return NotFound();

                _basketService.Checkout(basketCheckout);
                
                return Ok();
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

    }
}