using BooksShop.Catalog.Application.DTOs;
using BooksShop.Catalog.Application.Helpers.Exceptions;
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
            catch(BadRequestException e)
            {
                return BadRequest("Error: " + e.Message);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int bookId)
        {
            try
            {
                var response = await _bookService.Delete(bookId);

                if(response == false)
                    return BadRequest();

                return Ok("Success");
            }
            catch(BadRequestException e)
            {
                return BadRequest("Error: " + e.Message);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
    }
}