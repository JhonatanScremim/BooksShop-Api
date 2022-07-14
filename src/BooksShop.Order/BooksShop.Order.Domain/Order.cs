namespace BooksShop.Order.Domain
{
    public class Order
    {
        public string UserName { get; set; }
        public double TotalPrice { get; set; }
        public string BooksIds { get; set; }

        //Billing Address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Order(string userName, double totalPrice, string booksIds, string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
        {
            UserName = userName;
            TotalPrice = totalPrice;
            BooksIds = booksIds;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }
    }
}