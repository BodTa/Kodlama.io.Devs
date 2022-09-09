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

namespace Application.Features.SocialLinks.Commands.DeleteSocialLink;

public class DeleteSocialLinkCommand:IRequest<DeletedSocialLinkDto>
{
    public int Id { get; set; }
}
public class DeleteSocialLinkCommandHanler : IRequestHandler<DeleteSocialLinkCommand, DeletedSocialLinkDto>
{
    private readonly IMapper mapper;
    private readonly ISocialLinkRepository socialLinkRepository;
    private readonly SocialLinkBusinessRules socialLinkBusinessRules;

    public DeleteSocialLinkCommandHanler(IMapper mapper, ISocialLinkRepository socialLinkRepository, SocialLinkBusinessRules socialLinkBusinessRules)
    {
        this.mapper = mapper;
        this.socialLinkRepository = socialLinkRepository;
        this.socialLinkBusinessRules = socialLinkBusinessRules;
    }

    public async Task<DeletedSocialLinkDto> Handle(DeleteSocialLinkCommand request, CancellationToken cancellationToken)
    {
        
        SocialLink socialLink = await socialLinkRepository.GetAsync(s => s.Id == request.Id);
        await socialLinkBusinessRules.SocialLinkMustBeExistWhenRequested(socialLink);
        SocialLink deletedLink = await socialLinkRepository.DeleteAsync(socialLink);
        var mappedLink = mapper.Map<DeletedSocialLinkDto>(deletedLink);
        return mappedLink;
    }
}

