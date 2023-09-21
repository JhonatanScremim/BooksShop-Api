using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.Helpers.Exceptions;
using BooksShop.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BooksShop.Catalog.Infra.Helpers.Models;

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

        [HttpGet("paginated")]
        public IActionResult GetPaginated([FromQuery] PageParams pageParams)
        {
            try
            {
                var response = _bookService.GetPaginated(pageParams);
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
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

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateAsync(int bookId, [FromBody] BookDTO model)
        {
            try
            {
                var response = await _bookService.UpdateAsync(bookId, model);

                return Ok("Updated");
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

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteAsync(int bookId)
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