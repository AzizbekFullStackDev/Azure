using Azure.Data.DbContexts;
using Azure.Data.IRepositories;
using Azure.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace Azure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        AppDbContext _dbContext;
        DbSet<TEntity> _dbSet;  
        public Repository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<TEntity>();    
        }


        public async Task<TEntity> InsertAsync(TEntity entity)
        {
             await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            var result = await this._dbSet.Where(e => e.Id == Id).FirstOrDefaultAsync();
            result.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public IQueryable<TEntity> SelectAll() => this._dbSet;                                                                                          

        public async Task<TEntity> SelectByIdAsync(long Id)
        {
            var result = await this._dbSet.Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = (this._dbContext.Update(entity)).Entity;
            await this._dbContext.SaveChangesAsync();
            return result;
        }
    }
}
