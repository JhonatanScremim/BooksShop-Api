using BooksShop.Catalog.Application.DTOs;
using BooksShop.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BookDTO model)
        {
            try
            {
                return Ok(await _bookService.CreateAsync(model));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
    }
}