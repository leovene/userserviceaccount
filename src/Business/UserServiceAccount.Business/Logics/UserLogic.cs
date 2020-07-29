using FluentValidation;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Business;
using UserServiceAccount.Domain.Interfaces.Infra;

namespace UserServiceAccount.Business.Logics
{
    public class UserLogic : BaseLogic<UserEntity>, IUserLogic
    {
        public UserLogic(IUserRepository repository, IValidator<UserEntity> validator) : base(repository, validator)
        {
        }
    }
}
