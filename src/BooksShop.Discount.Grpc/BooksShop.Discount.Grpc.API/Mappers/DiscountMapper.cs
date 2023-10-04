using AutoMapper;
using BooksShop.Discount.Grpc.API.Protos;
using BooksShop.Discount.Grpc.Repository.Models;

namespace BooksShop.Discount.Grpc.API.Mappers
{
    public class DiscountMapper : Profile
    {
        public DiscountMapper()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}