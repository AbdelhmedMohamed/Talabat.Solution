using System.ComponentModel.DataAnnotations;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.APIS.DTOs
{
    public class OrderDto
    {

        [Required]
        public string BuyerEmail {  get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int  DelivaryMethodId { get; set; }

        public AddressDto shippingAddress { get; set; }
    }
}
