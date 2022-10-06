using Application.Features.UserOperationClaims.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Models
{
    public class GetListUserOperationClaimModel:BasePageableModel
    {
        public List<GetListUserOperationClaimDto> Items { get; set; }
    }
}
