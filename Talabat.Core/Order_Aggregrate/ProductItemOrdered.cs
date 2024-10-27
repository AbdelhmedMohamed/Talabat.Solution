using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Order_Aggregrate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int producrId, string productName, string productUrl)
        {
            ProducrId = producrId;
            ProductName = productName;
            ProductUrl = productUrl;
        }

        public int ProducrId { get; set; }  //2

        public string ProductName { get; set; }  //Latte
        public string ProductUrl { get; set; } //Ay Soooora

    }
}
