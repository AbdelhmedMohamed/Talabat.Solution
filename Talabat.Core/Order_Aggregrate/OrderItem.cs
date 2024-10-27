using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Order_Aggregrate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            this.product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered product {  get; set; }

        public decimal  Price { get; set; }

        public int  Quantity { get; set; }




    }
}
