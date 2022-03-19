using BooksShop.Basket.Domain;
using BooksShop.Basket.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketAsync(string userName)
        {
            try
            {
                return Ok(await _basketRepository.GetBasketAsync(userName) ??  new ShoppingCart(userName));
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasketAsync(ShoppingCart basket)
        {
            try
            {
                return Ok(await _basketRepository.UpdateBasketAsync(basket));
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
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