using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Dtos
{
    public   class UpdatedUserOperationClaimDto
    {
        public int OperationClaimId { get; set; }
        public int UserId { get; set; }
    }
}
