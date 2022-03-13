using BooksShop.Catalog.Domain.Enums;

namespace BooksShop.Catalog.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public CoverType CoverTypeId { get; set; }
        public Language LanguageId { get; set; }
        public int Pages { get; set; }
        public Publisher? Publisher { get; set; }
        public int PublisherId { get; set; }
        public DateTime PublicationDate { get; set; }
        public double DimensionX { get; set; }
        public double DimensionY { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<AuthorBook>? AuthorBooks { get; set; }
        
        
    }
}