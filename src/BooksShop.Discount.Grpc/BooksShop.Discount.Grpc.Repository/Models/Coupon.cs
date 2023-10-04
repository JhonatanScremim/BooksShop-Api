namespace BooksShop.Discount.Grpc.Repository.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public Coupon(int id, int bookId, string bookName, string description, int amount)
        {
            Id = id;
            BookId = bookId;
            BookName = bookName;
            Description = description;
            Amount = amount;
        }
    }
}