using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity ;


        //public IGenericRepository<Product> ProductsRepo { get; set; }
        //public IGenericRepository<ProductBrand> BrandsRepo { get; set; }
        //public IGenericRepository<ProductCategory> CategoriesRepo { get; set; }
        //public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }
        //public IGenericRepository<OrderItem> OrderItemsRepo { get; set; }
        //public IGenericRepository<Order> OrdersRepo { get; set; }

        Task<int> CompleteAsync();



    }
}
