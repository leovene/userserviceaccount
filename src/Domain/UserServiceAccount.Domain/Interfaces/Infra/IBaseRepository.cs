using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Entities;

namespace UserServiceAccount.Domain.Interfaces.Infra
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> SelectAll();

        Task<TEntity> Select(Guid id);

        Guid Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(Guid Id);

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
