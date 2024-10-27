using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Order_Aggregrate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string puyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            PuyerEmail = puyerEmail;
           
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string  PuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } 

        public Address ShippingAddress { get; set; }

        public int? DeliveryMethodId { get; set; } //FK
        public DeliveryMethod? DeliveryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set;} = new List<OrderItem>(); //navigation prop

        public decimal SubTotal { get; set; }

        //public decimal Total  { get; }

        public decimal GetTotal()
        => SubTotal +  DeliveryMethod.Cost ;


        public string?  PaymentIntentId { get; set; }
    }
}
