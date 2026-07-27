using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Application.Specifications
{
    public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {

        #region Where
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public BaseSpecifications(Expression<Func<TEntity, bool>> criteria = null)
        {
            Criteria = criteria;
        }
        #endregion


        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];

        public void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }
        #endregion

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public void AddOrderBy(Expression<Func<TEntity, object>>? orderBy)
            => OrderBy = orderBy;

        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }
        public void AddOrderByDesc(Expression<Func<TEntity, object>>? orderByDesc)
            => OrderByDesc = orderByDesc;
    }
}
