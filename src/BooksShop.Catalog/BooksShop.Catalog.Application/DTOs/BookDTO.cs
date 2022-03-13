using BooksShop.Catalog.Domain.Enums;

namespace BooksShop.Catalog.Application.DTOs
{
    public class BookDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public CoverType CoverTypeId { get; set; }
        public Language LanguageId { get; set; }
        public int Pages { get; set; }
        public PublisherDTO? Publisher { get; set; }
        public int PublisherId { get; set; }
        public DateTime PublicationDate { get; set; }
        public double DimensionX { get; set; }
        public double DimensionY { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<AuthorDTO>? Authors { get; set; }
    }
}