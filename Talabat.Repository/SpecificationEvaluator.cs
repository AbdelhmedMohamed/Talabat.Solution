using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> innerQuery , ISpecification<TEntity> spec)
        {
            var query = innerQuery;
            
            if (spec.Critria is not null )
            {
                query = query.Where(spec.Critria);
            }

            query = spec.Includes.Aggregate(query, (currentQuery,includesExpression)=> currentQuery.Include(includesExpression));

            return query;


        }

    }
}
