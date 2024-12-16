using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {

        public OrderSpecifications(string buyerEmail) : base(o => o.PuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderBy(o => o.OrderDate);

        }

        public OrderSpecifications(int orderId , string buyerEmail)
            :base(o =>o.Id == orderId && o.PuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

        }


    }
}
