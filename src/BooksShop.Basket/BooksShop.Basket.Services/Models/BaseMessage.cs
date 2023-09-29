namespace BooksShop.Basket.Services.Models
{
    public class BaseMessage
    {
        public long Id { get; set; }
        public DateTime MessageCreated { get; set; } = DateTime.Now;
    }
}