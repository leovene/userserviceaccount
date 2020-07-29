using UserServiceAccount.Domain.Entities;

namespace UserServiceAccount.Domain.Interfaces.Infra
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
    }
}
