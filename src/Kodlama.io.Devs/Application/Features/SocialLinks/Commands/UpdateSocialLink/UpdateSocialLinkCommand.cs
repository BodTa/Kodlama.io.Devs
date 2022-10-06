using Application.Features.SocialLinks.Dtos;
using Application.Features.SocialLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Commands.UpdateSocialLink;

public class UpdateSocialLinkCommand:IRequest<UpdatedSocialLinkDto>,ISecuredRequest
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int UserId { get; set; }
    public string[] Roles { get; } = { "user" };

}
public class UpdateSocialLinkCommandHandler : IRequestHandler<UpdateSocialLinkCommand, UpdatedSocialLinkDto>
{
    private readonly IMapper mapper;
    private readonly ISocialLinkRepository socialLinkRepository;
    private readonly SocialLinkBusinessRules socialLinkBusinessRules;

    public UpdateSocialLinkCommandHandler(IMapper mapper, ISocialLinkRepository socialLinkRepository, SocialLinkBusinessRules socialLinkBusinessRules)
    {
        this.mapper = mapper;
        this.socialLinkRepository = socialLinkRepository;
        this.socialLinkBusinessRules = socialLinkBusinessRules;
    }

    public async Task<UpdatedSocialLinkDto> Handle(UpdateSocialLinkCommand request, CancellationToken cancellationToken)
    {

        var socialLink = mapper.Map<SocialLink>(request);
        socialLinkBusinessRules.SocialLinkMustBeExistWhenRequested(socialLink);
        SocialLink updatedLink = await socialLinkRepository.UpdateAsync(socialLink);
        var mappedLink = mapper.Map<UpdatedSocialLinkDto>(updatedLink);
        return mappedLink;
    }
}
