namespace BooksShop.Basket.Domain
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
    }
}