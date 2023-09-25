using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorController(IAuthorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorDTO dto)
        {
            try
            {
                var response = await _service.CreateAsync(dto);
                
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