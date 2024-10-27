using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;
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

            CreateMap<CustomerBasketDto, customerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            
            CreateMap<AddressDto,Address>();

          
        }


    }
}
