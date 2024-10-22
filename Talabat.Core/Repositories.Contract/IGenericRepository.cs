using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
         Task<T> GetAsync (int id);

        Task<IReadOnlyList<T>> GetAllAsync();


        Task<IReadOnlyList<T>> GetAllWhithSpacAsync(ISpecification<T> spac);

        Task<T> GetWhithSpacAsync(ISpecification<T> spac);

        Task<int> GetCountAsync(ISpecification<T> spac);



    }

}
