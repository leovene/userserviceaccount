using UserServiceAccount.Data.Contexts;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Infra;

namespace UserServiceAccount.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(SqlServerContext context) : base(context)
        {

        }
    }
}
