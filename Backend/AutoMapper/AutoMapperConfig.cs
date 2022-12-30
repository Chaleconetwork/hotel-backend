using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.AutoMapper
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
