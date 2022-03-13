namespace BooksShop.Catalog.Application.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BookDTO>? Books { get; set; }
    }
}