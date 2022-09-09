using Application.Features.SocialLinks.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Queries.GetListSocialLink;

public class GetListSocialLinkQuery:IRequest<GetListSocialLinkModel>
{
    public PageRequest PageRequest { get; set; }
}
public class GetListSocialLinkQueryHandler : IRequestHandler<GetListSocialLinkQuery, GetListSocialLinkModel>
{
    private readonly IMapper mapper;
    private readonly ISocialLinkRepository socialLinkRepository;

    public GetListSocialLinkQueryHandler(IMapper mapper, ISocialLinkRepository socialLinkRepository)
    {
        this.mapper = mapper;
        this.socialLinkRepository = socialLinkRepository;
    }

    async Task<GetListSocialLinkModel> IRequestHandler<GetListSocialLinkQuery, GetListSocialLinkModel>.Handle(GetListSocialLinkQuery request, CancellationToken cancellationToken)
    {
        IPaginate<SocialLink> socialLinks = await socialLinkRepository.GetListAsync(include:
            m => m.Include(a => a.AppUser),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        var mappedLinks = mapper.Map<GetListSocialLinkModel>(socialLinks);
        return mappedLinks;
    }
}
