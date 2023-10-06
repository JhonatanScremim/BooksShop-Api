using System.Diagnostics.Contracts;
using BooksShop.Discount.Grpc.Repository.Models;
using BooksShop.Discount.Grpc.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace BooksShop.Discount.Grpc.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BooksShop-Discount-Postgres");
        }

        public async Task<Coupon> GetAsync(int bookId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<Coupon>(
                    "select * from Coupon where bookId = @BookId",
                    new { BookId = bookId });
            }
        }

        public async Task<bool> CreateAsync(Coupon coupon)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var affectedLines = await conn.ExecuteAsync
                ("insert into Coupon values (Default, @BookId, @BookName, @Description, @Amount)",
                new {BookId = coupon.BookId, BookName = coupon.BookName, Description = coupon.Description, Amount = coupon.Amount});

                if (affectedLines < 1)
                    return false;
                
                return true;
            }
        }
        public async Task<bool> UpdateAsync(Coupon coupon)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var affectedLines = await conn.ExecuteAsync
                ("update Coupon set BookName = @BookName, Description = @Description, Amount = @Amount where Id = @Id",
                new { BookName = coupon.BookName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id});
            
                if (affectedLines < 1)
                    return false;
                
                return true;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var affectedLines = await conn.ExecuteAsync
                ("delete from Coupon where Id = @id",
                new {Id = id});

                if (affectedLines < 1)
                    return false;
                
                return true;
            }
        }
    }
}