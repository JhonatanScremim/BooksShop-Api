using BooksShop.Catalog.Infra.DTOs;
using BooksShop.Catalog.Infra.Helpers.Exceptions;
using BooksShop.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BooksShop.Catalog.Infra.Helpers.Models;

namespace BooksShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _service;

        public PublisherController(IPublisherService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PublisherDTO dto)
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