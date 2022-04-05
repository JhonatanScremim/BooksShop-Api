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

        [HttpGet("{bookName}")]
        public async Task<IActionResult> GetAsync(string bookName)
        {
            try
            {
                return Ok(await _repository.GetAsync(bookName));
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
    }
}