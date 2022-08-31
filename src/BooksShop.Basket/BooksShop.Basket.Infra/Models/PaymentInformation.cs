namespace BooksShop.Basket.Infra.Models
{
    public class PaymentInformation
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public string PaymentMethod { get; set; }

        public PaymentInformation(string cardName, string cardNumber, string expiration, string cVV, string paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cVV;
            PaymentMethod = paymentMethod;
        }
    }
}