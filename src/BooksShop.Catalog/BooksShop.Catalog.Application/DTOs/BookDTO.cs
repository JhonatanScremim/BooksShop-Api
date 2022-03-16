using System;
using System.ComponentModel.DataAnnotations;
using BooksShop.Catalog.Domain.Enums;

namespace BooksShop.Catalog.Application.DTOs
{
    public class BookDTO
    {
        [Required, StringLength(100, MinimumLength = 4)]
        public string? Title { get; set; }
        
        [Required, StringLength(200, MinimumLength = 4)]
        public string? Description { get; set; }
        [Range(1, 2)]
        public CoverType CoverTypeId { get; set; }
        [Range(1,3)]
        public Language LanguageId { get; set; }
        public int Pages { get; set; }
        [Range(1, 1000)]
        public int PublisherId { get; set; }
        public DateTime PublicationDate { get; set; }
        public double DimensionX { get; set; }
        public double DimensionY { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public List<int>? AuthorsIds { get; set; }
    }
}