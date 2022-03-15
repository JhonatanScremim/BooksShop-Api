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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _bookService.GetAllAsync();

                if(response == null)
                    return NoContent();
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BookDTO model)
        {
            try
            {
                var response = await _bookService.CreateAsync(model);

                return Created("", response);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
    }
}