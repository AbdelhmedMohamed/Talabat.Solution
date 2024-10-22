using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }
        public int Skip { get ; set  ; } //0
        public int Take { get ; set  ; } //0
        public bool IsPaginationEnabled { get; set; } = false;

        public BaseSpecifications()
        {
            //Critria = null
        }

        public BaseSpecifications(Expression<Func<T, bool>> critriaexpression)
        {
            Critria = critriaexpression;
        }


        public void AddOrderBy(Expression<Func<T, object>> orderByExperission)
        { 
            OrderBy = orderByExperission;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExperission)
        {
            OrderBy = orderByDescExperission;
        }

        public void ApplyPagination(int take , int skip )
        {
            IsPaginationEnabled = true;
            Take = take;
            Skip = skip;
        }



    }
}
