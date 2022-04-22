using BooksShop.Discount.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetAsync(int bookId)
        {
            try
            {
                return Ok(await _repository.GetAsync(bookId));
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
    }
}