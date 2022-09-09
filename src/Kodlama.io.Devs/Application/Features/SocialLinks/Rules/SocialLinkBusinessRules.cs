using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Rules
{
    public class SocialLinkBusinessRules
    {
        private readonly ISocialLinkRepository socialLinkRepository;

        public SocialLinkBusinessRules(ISocialLinkRepository socialLinkRepository)
        {
            this.socialLinkRepository = socialLinkRepository;
        }
        public async Task SocialLinkCannotBeDuplicated(string Url)
        {
            SocialLink socialLink = await socialLinkRepository.GetAsync(s => s.Url == Url);
            if(socialLink != null) throw new BusinessException("Social Link cannot be duplicated");
        }
        public async Task SocialLinkMustBeExistWhenRequested(SocialLink socialLink)
        {
            if (socialLink == null) throw new BusinessException("Social Link does not exist");
        }
    }
}
