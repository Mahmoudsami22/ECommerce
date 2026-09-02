using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.Infrastructure.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery,
            ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var query = InputQuery;
            if (specifications.IncludeExpressions.Count > 0)
            {
                query = specifications.IncludeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            }
            if(specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }
            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return query;
        }
    }
}
