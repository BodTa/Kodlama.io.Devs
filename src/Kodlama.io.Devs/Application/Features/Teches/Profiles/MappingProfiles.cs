using Application.Features.Teches.Commands.CreateTech;
using Application.Features.Teches.Commands.DeleteTech;
using Application.Features.Teches.Commands.UpdateTech;
using Application.Features.Teches.Dtos;
using Application.Features.Teches.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teches.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tech,GetListTechDto>().ForMember(c=>c.LanguageName,opt=>opt.MapFrom(c=>c.Language.Name)).ReverseMap();
            CreateMap<IPaginate<Tech>, GetListTechModel>().ReverseMap();
            CreateMap<Tech,CreatedTechDto>().ReverseMap();
            CreateMap<Tech, CreateTechCommand>().ReverseMap();
            CreateMap<Tech,UpdatedTechDto>().ReverseMap();
            CreateMap<Tech,UpdateTechCommand>().ReverseMap();
            CreateMap<Tech,DeletedTechDto>().ReverseMap();
            CreateMap<Tech, DeleteTechCommand>().ReverseMap();
            //CreateMap<Tech>().ReverseMap();
            //CreateMap<Tech>().ReverseMap();
        }
    }
}
