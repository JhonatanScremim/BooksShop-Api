namespace BooksShop.Order.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double TotalPrice { get; set; }
        public string UserInfo { get; set; }
        public string BooksIds { get; set; }
        public string Address { get; set; }
        public string PaymentInfo { get; set; }

        public Order(string userName, double totalPrice, string userInfo, string booksIds, string address, string paymentInfo)
        {
            UserName = userName;
            TotalPrice = totalPrice;
            UserInfo = userInfo;
            BooksIds = booksIds;
            Address = address;
            PaymentInfo = paymentInfo;
        }
    }
}