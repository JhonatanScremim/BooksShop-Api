namespace BooksShop.Discount.Domain
{
    public class Coupon
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public Coupon(int id, string bookName, string description, int amount)
        {
            Id = id;
            BookName = bookName;
            Description = description;
            Amount = amount;
        }
    }
}