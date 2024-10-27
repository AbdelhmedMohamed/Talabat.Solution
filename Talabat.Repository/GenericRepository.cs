using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext  dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Product))
            {
                return  (IReadOnlyList<T>) await _dbcontext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            }
           return await _dbcontext.Set<T>().ToListAsync();
        }

     
        public async Task<T> GetAsync(int id)
        {
            
           if (typeof(T) == typeof(Product))
           {
                return await _dbcontext.Set<Product>().Where(p=>p.Id == id).Include(p=>p.Brand).Include(p=>p.Category).FirstOrDefaultAsync() as T ;
            }
                return await _dbcontext.Set<T>().FindAsync(id);
        }

        ///================================


        public async Task<IReadOnlyList<T>> GetAllWhithSpacAsync(ISpecification<T> spac)
        {
            return await ApplySpacification(spac).ToListAsync();
        }

       

        public async Task<T> GetWhithSpacAsync(ISpecification<T> spac)
        {
            return await ApplySpacification(spac).FirstOrDefaultAsync();  
        }

        public async Task<int> GetCountAsync(ISpecification<T> spac)
        {
           return await ApplySpacification(spac).CountAsync();
        }


        private  IQueryable<T> ApplySpacification(ISpecification<T> spac)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spac);
        }

        public async Task AddAsync(T entity)
        {
            await _dbcontext.AddAsync(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbcontext.Update(entity);
        }

        public void DeleteAsync(T entity)
        {
            _dbcontext.Remove(entity);
        }
    }
}
