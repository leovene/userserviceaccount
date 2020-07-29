using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Business;
using UserServiceAccount.Domain.Interfaces.Infra;

namespace UserServiceAccount.Business.Logics
{
    public abstract class BaseLogic<TEntity> : IBaseLogic<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IValidator<TEntity> _validator;

        public BaseLogic(IBaseRepository<TEntity> repository, IValidator<TEntity> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _repository.SelectAll().ConfigureAwait(false);

        public virtual async Task<TEntity> Get(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return await _repository.Select(id).ConfigureAwait(false);
        }

        public virtual async Task<Guid> Post(TEntity entity)
        {
            Validate(entity, _validator);

            _repository.Insert(entity);

            await _repository.SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }

        public virtual async Task<int> Put(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Validate(entity, _validator);

            _repository.Update(entity);

            return await _repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> Delete(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            _repository.Delete(id);

            return await _repository.SaveChangesAsync().ConfigureAwait(false);
        }

        private void Validate(TEntity entity, IValidator<TEntity> validator)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            validator.ValidateAndThrow(entity);
        }
    }
}
