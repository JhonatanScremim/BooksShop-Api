namespace BooksShop.Catalog.Infra.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BookViewModel>? Books { get; set; }
    }
}