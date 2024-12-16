using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Order_Aggregrate;
using static System.Net.WebRequestMethods;

namespace Talabat.APIS.Helpers
{
    public class MappingProfiles :Profile
    {
      

        public MappingProfiles()
        {
         

            CreateMap<Product, ProductDto>()
                     .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                     .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                     .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>()) ;

            CreateMap<CustomerBasketDto, customerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();

            CreateMap<AddressDto, Core.Entities.Identity.Address>().ReverseMap();
                    //.ForMember(d => d.FirstName, o => o.MapFrom(s => s.FName))
                    //.ForMember(d => d.LastName, o => o.MapFrom(s => s.LName));
                    



            CreateMap<Order, OrderToReturnDto>()
                    .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                    .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(d => d.ProducrId, o => o.MapFrom(s => s.product.ProducrId))
                    .ForMember(d => d.ProductName, o => o.MapFrom(s => s.product.ProductName))
                    .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.product.ProductUrl))
                    .ForMember(d => d.ProductUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());




        }


    }
}
