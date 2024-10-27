using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbcontext;

        // private Dictionary<string,GenericRepository<BaseEntity>> _repositories;

        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;

            _repositories = new Hashtable();

            //ProductsRepo = new GenericRepository<Product>(_dbcontext);
            //BrandsRepo = new GenericRepository<ProductBrand>(_dbcontext);
            //CategoriesRepo = new GenericRepository<ProductCategory>(_dbcontext);
            //DeliveryMethodsRepo = new GenericRepository<DeliveryMethod>(_dbcontext);
            //OrderItemsRepo = new GenericRepository<OrderItem>(_dbcontext);
            //OrdersRepo = new GenericRepository<Order>(_dbcontext);

        }

        //public IGenericRepository<Product> ProductsRepo { get ; set ; }
        //public IGenericRepository<ProductBrand> BrandsRepo { get ; set ; }
        //public IGenericRepository<ProductCategory> CategoriesRepo { get ; set ; }
        //public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get ; set ; }
        //public IGenericRepository<OrderItem> OrderItemsRepo { get ; set ; }
        //public IGenericRepository<Order> OrdersRepo { get ; set ; }

        public async Task<int> CompleteAsync()
        {
           return await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _dbcontext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbcontext);
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;

        }
    }
}
