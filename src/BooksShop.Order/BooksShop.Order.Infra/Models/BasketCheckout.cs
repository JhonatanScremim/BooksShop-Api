namespace BooksShop.Order.Infra.Models
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public double TotalPrice { get; set; }
        public List<int> BooksIds { get; set; }
        public UserInformation UserInformation { get; set; }
        public Address Address { get; set; }
        public PaymentInformation PaymentInformation { get; set; }

        public BasketCheckout(string userName, double totalPrice, List<int> booksIds, UserInformation userInformation, Address address, PaymentInformation paymentInformation)
        {
            UserName = userName;
            TotalPrice = totalPrice;
            BooksIds = booksIds;
            UserInformation = userInformation;
            Address = address;
            PaymentInformation = paymentInformation;
        }
    }
}