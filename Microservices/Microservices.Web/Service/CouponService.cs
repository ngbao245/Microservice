using System;
using Microservices.Web.Models;
using Microservices.Web.Service.IService;
using Microservices.Web.Utility;
using static Microservices.Web.Utility.StaticDetail;

namespace Microservices.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.ApiType.POST,
                Data = couponDto,
                Url = StaticDetail.CouponAPIBase + "coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.ApiType.DELETE,
                Url = StaticDetail.CouponAPIBase + "coupon/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.ApiType.GET,
                Url = StaticDetail.CouponAPIBase + "coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.ApiType.GET,
                Url = StaticDetail.CouponAPIBase + "coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.ApiType.GET,
                Url = StaticDetail.CouponAPIBase + "coupon/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.ApiType.PUT,
                Data = couponDto,
                Url = StaticDetail.CouponAPIBase + "coupon"
            });
        }
    }
}
