namespace BooksShop.Catalog.Infra.DTOs.DiscountAPI
{
    public class CouponDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public CouponDTO(int id, int bookId, string bookName, string description, int amount)
        {
            Id = id;
            BookId = bookId;
            BookName = bookName;
            Description = description;
            Amount = amount;
        }
    }
}