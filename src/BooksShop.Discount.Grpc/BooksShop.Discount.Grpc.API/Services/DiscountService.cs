using AutoMapper;
using BooksShop.Discount.Grpc.API.Protos;
using BooksShop.Discount.Grpc.Repository.Interfaces;
using BooksShop.Discount.Grpc.Repository.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Discount.Grpc.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            try
            {
                var coupon = await _repository.GetAsync(request.BookId);
                if(coupon == null)
                    throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));

                return _mapper.Map<CouponModel>(coupon);   
            }
            catch(Exception e)
            {
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            try
            {
                await _repository.CreateAsync(_mapper.Map<Coupon>(request.Coupon));
                return request.Coupon;
            }
            catch(Exception e)
            {
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            try
            {
                await _repository.UpdateAsync(_mapper.Map<Coupon>(request.Coupon));
                return request.Coupon;
            }
            catch(Exception e)
            {
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(request.Id);
                return new DeleteDiscountResponse(){
                    Success = deleted
                };
            }
            catch(Exception e)
            {
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }
    }
}