using Application.Features.SocialLinks.Dtos;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialLinks.Models
{
    public class GetListSocialLinkModel:BasePageableModel
    {
        public List<GetListSocialLinkDto> Items { get; set; }
    }
}
