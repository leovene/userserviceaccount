using AutoMapper;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.ViewModels;

namespace UserServiceAccount.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, UserEntity>().ReverseMap();
        }
    }
}
