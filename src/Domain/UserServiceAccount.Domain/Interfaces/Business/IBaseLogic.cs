using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Entities;

namespace UserServiceAccount.Domain.Interfaces.Business
{
    public interface IBaseLogic<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Get(Guid id);

        Task<Guid> Post(TEntity model);

        Task<int> Put(TEntity model);

        Task<int> Delete(Guid id);
    }
}
