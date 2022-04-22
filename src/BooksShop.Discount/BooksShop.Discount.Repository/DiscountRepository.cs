using System.Diagnostics.Contracts;
using BooksShop.Discount.Domain;
using BooksShop.Discount.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BooksShop.Discount.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BooksShop-Discount");
        }

        public async Task<Coupon> GetAsync(int bookId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<Coupon>(
                    "select * from Coupon where bookId = @BookId",
                    new { BookId = bookId });
            }
        }

        public async Task<bool> CreateAsync(Coupon coupon)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var affectedLines = await conn.ExecuteAsync
                ("insert into Coupon values (@BookName, @Description, @Amount))",
                new {BookName = coupon.BookName, Description = coupon.Description, Amount = coupon.Amount});

                if (affectedLines < 1)
                    return false;
                
                return true;
            }
        }
        public async Task<bool> UpdateAsync(Coupon coupon)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var affectedLines = await conn.ExecuteAsync
                ("update Coupon set BookName = @BookName, Description = @Description, Amount = @Amount where Id = @Id",
                new { BookName = coupon.BookName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id});
            
                if (affectedLines < 1)
                    return false;
                
                return true;
            }
        }

        public async Task<bool> DeleteAsync(string bookName)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var affectedLines = await conn.ExecuteAsync
                ("delete Coupon where BookName = @BookName",
                new {BookName = bookName});

                if (affectedLines < 1)
                    return false;
                
                return true;
            }
        }
    }
}