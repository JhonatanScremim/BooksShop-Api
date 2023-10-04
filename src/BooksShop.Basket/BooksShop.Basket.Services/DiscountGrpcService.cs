using BooksShop.Discount.Grpc.API.Protos;

namespace BooksShop.Basket.Services
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            _discountService = discountService;
        }

        public async Task<CouponModel> GetDiscount(int bookId)
        {
            var discountReqeust = new GetDiscountRequest
            {
                BookId = bookId
            };

            return await _discountService.GetDiscountAsync(discountReqeust);
        }
    }
}