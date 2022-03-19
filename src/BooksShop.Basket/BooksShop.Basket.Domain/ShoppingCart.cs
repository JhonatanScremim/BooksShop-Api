namespace BooksShop.Basket.Domain
{
    public class ShoppingCart
    {
        public string? UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public double TotalPrice
        {
            get 
            {
                double totalPrice = 0;
                foreach(var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;    
                }
                return totalPrice;
            }
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}