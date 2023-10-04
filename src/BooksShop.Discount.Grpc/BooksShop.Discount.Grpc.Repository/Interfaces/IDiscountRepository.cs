using BooksShop.Discount.Grpc.Repository.Models;

namespace BooksShop.Discount.Grpc.Repository.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetAsync(int bookId);
        Task<bool> CreateAsync(Coupon coupon);
        Task<bool> UpdateAsync(Coupon coupon);
        Task<bool> DeleteAsync(int id);
    }
}