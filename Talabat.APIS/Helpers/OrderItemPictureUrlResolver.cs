using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIS.DTOs;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.APIS.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.product.ProductUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.product.ProductUrl}";
            }
            return string.Empty;
        }
    }
}
