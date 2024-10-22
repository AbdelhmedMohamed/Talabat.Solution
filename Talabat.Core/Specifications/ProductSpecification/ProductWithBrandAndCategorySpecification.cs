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
        public ProductWithBrandAndCategorySpecification(ProductSpecParams spec) : base( p=>

                (string.IsNullOrEmpty(spec.Search) || p.Name.ToLower().Contains(spec.Search.ToLower()))  &&

                (!spec.BrandId.HasValue || p.BrandId == spec.BrandId.Value ) &&

              (!spec.CategoryId.HasValue || p.CategoryId == spec.CategoryId.Value )

            )
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
            if (!string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }

            }

            else
            {
                AddOrderBy(p => p.Name);  
            }

            ApplyPagination( (spec.PageIndex -1) * spec.PageSize  , spec.PageSize ); 

        }
        public ProductWithBrandAndCategorySpecification(int id) :base(p => p.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }


    }
}
