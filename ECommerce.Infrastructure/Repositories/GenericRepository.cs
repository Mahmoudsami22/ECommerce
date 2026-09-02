using ECommerce.Domain.Commen;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct); 
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
            return await dbContext.Set<TEntity>().FindAsync(id, ct);
        }

        
    }
}
