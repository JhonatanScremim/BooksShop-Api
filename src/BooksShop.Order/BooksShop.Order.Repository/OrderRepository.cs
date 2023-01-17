using BooksShop.Order.Repository.Context;
using BooksShop.Order.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksShop.Order.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<DataContext> _context;

        public OrderRepository(DbContextOptions<DataContext> context)
        {
            _context = context;
        }
        public async Task<bool> CreateOrder(Domain.Order order)
        {
            await using var _db = new DataContext(_context);
            if (order == null || _db.Order == null)
                return false;
                
            _db.Order.Add(order);

            return (await _db.SaveChangesAsync() > 0);
        }
    }
}