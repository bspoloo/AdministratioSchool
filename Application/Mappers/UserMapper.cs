using AdministratioSchool.Domain.DTO.In;
using AdministratioSchool.Domain.DTO.Out;
using AdministratioSchool.Domain.Entities;
using AutoMapper;

namespace AdministratioSchool.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserOutDTO>();
            CreateMap<UserInDTO,User>();
        }
    }
}
