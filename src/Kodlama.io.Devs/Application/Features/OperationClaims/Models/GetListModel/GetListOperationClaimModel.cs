using Application.Features.OperationClaims.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Models.GetListModel
{
    public class GetListOperationClaimModel:BasePageableModel
    {
        public List<GetListOperationClaimDto> Items { get; set; }
    }
}
