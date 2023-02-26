using AutoMapper;
using SAQAYA.UserAPI.Entities.Entities;
using SAQAYA.UserAPI.Models;

namespace SAQAYA.UserAPI.Web
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserModel, User>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserResponse>();
        }
    }
}
