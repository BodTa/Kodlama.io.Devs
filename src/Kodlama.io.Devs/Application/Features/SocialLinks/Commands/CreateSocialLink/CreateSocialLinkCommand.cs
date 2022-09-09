using Application.Features.SocialLinks.Dtos;
using Application.Features.SocialLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Commands.CreateSocialLink;

public class CreateSocialLinkCommand:IRequest<CreatedSocialLinkDto>
{
    public string Url { get; set; }
    public int UserId { get; set; }
}
public class CreateSocialLinkCommandHandler : IRequestHandler<CreateSocialLinkCommand, CreatedSocialLinkDto>
{
    private readonly IMapper mapper;
    private readonly ISocialLinkRepository socialLinkRepository;
    private readonly SocialLinkBusinessRules socialLinkBusinessRules;

    public CreateSocialLinkCommandHandler(IMapper mapper, ISocialLinkRepository socialLinkRepository, SocialLinkBusinessRules socialLinkBusinessRules)
    {
        this.mapper = mapper;
        this.socialLinkRepository = socialLinkRepository;
        this.socialLinkBusinessRules = socialLinkBusinessRules;
    }

    public async Task<CreatedSocialLinkDto> Handle(CreateSocialLinkCommand request, CancellationToken cancellationToken)
    {
        await socialLinkBusinessRules.SocialLinkCannotBeDuplicated(request.Url);
        var socialLink = mapper.Map<SocialLink>(request);
        SocialLink createdLink = await socialLinkRepository.AddAsync(socialLink);
        var mappedLink = mapper.Map<CreatedSocialLinkDto>(createdLink);
        return mappedLink;
    }
}
