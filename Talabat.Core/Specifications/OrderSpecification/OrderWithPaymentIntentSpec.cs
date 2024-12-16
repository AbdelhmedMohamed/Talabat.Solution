using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderWithPaymentIntentSpec : BaseSpecifications<Order> 
    {
        public OrderWithPaymentIntentSpec(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }

    }
}
