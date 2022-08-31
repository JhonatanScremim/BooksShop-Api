namespace BooksShop.Order.Repository.Interfaces
{
    public interface IOrderRepository
    {
         Task<bool> CreateOrder(Domain.Order order);
    }
}