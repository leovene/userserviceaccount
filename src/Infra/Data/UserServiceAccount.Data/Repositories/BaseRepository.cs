using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Infra;

namespace UserServiceAccount.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected DbContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = Context != null ? Context.Set<TEntity>() : throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<TEntity>> SelectAll() => await DbSet.ToArrayAsync().ConfigureAwait(false);

        public async Task<TEntity> Select(Guid id) => await DbSet.Where(e => e.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);

        public Guid Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbSet.Add(entity);

            return entity.Id;
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbSet.Update(entity);
        }

        public void Delete(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = DbSet.Where(e => e.Id == id).SingleOrDefault();

            DbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();

        public void Dispose() => Context.Dispose();
    }
}
