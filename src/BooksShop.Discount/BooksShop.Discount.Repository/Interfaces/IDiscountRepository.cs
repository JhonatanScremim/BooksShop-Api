using BooksShop.Discount.Domain;

namespace BooksShop.Discount.Repository.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetAsync(int bookId);
        Task<bool> CreateAsync(Coupon coupon);
        Task<bool> UpdateAsync(Coupon coupon);
        Task<bool> DeleteAsync(string bookName);
    }
}