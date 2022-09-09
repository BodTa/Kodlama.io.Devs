using Application.Features.Users.Dtos;
using AutoMapper;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User,CreatedUserDto>().ReverseMap();
            CreateMap<User, UpdatedUserDto>().ReverseMap();
            CreateMap<User, DeletedUserDto>().ReverseMap();

        }
    }
}
