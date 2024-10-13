using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithBrandAndCategorySpecification :BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecification() :base()
        {
            Includes.Add(p =>p.Brand);
            Includes.Add(p =>p.Category);
                
                
        }
        public ProductWithBrandAndCategorySpecification(int id) :base(p => p.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }


    }
}
