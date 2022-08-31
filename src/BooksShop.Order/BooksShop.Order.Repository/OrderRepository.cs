using BooksShop.Order.Repository.Context;
using BooksShop.Order.Repository.Interfaces;

namespace BooksShop.Order.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateOrder(Domain.Order order)
        {
            if (order == null || _context.Order == null)
                return false;

            _context.Order.Add(order);

            return (await _context.SaveChangesAsync() > 0);
        }
    }
}