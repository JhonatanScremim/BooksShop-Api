using Microsoft.EntityFrameworkCore;

namespace BooksShop.Order.Repository.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Domain.Order>? Order { get; set; }
    }
}