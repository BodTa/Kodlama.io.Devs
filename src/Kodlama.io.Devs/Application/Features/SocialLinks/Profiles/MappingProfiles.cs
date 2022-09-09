using Application.Features.SocialLinks.Commands.CreateSocialLink;
using Application.Features.SocialLinks.Commands.DeleteSocialLink;
using Application.Features.SocialLinks.Commands.UpdateSocialLink;
using Application.Features.SocialLinks.Dtos;
using Application.Features.SocialLinks.Models;
using Application.Features.SocialLinks.Queries.GetListSocialLink;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<SocialLink, GetListSocialLinkDto>().ForMember(s => s.FirstName, opt => opt.MapFrom(c => c.AppUser.FirstName))
            .ForMember(s => s.LastName, opt => opt.MapFrom(c => c.AppUser.LastName)).ReverseMap();
        CreateMap<IPaginate<SocialLink>, GetListSocialLinkModel>().ReverseMap();
        CreateMap<SocialLink, CreatedSocialLinkDto>().ReverseMap();
        CreateMap<SocialLink, CreateSocialLinkCommand>().ReverseMap();
        CreateMap<SocialLink, UpdatedSocialLinkDto>().ReverseMap();
        CreateMap<SocialLink, UpdateSocialLinkCommand>().ReverseMap();
        CreateMap<SocialLink, DeletedSocialLinkDto>().ReverseMap();
        CreateMap<SocialLink, DeleteSocialLinkCommand>().ReverseMap();

    }
}
