using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithFilterationForCountSpec : BaseSpecifications<Product>
    {
        public ProductWithFilterationForCountSpec(ProductSpecParams spec) : base(p=>

                 (string.IsNullOrEmpty(spec.Search) || p.Name.ToLower().Contains(spec.Search.ToLower())) &&

                (!spec.BrandId.HasValue || p.BrandId == spec.BrandId.Value) &&

              (!spec.CategoryId.HasValue || p.CategoryId == spec.CategoryId.Value)

            )
        {

            
        }

    }
}
