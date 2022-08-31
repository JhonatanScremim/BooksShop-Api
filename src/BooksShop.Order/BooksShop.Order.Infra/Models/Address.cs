namespace BooksShop.Order.Infra.Models
{
    public class Address
    {
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address(string addressLine, string country, string state, string zipCode)
        {
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }
    }
}