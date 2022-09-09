using Application.Features.Teches.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teches.Models
{
    public class GetListTechModel:BasePageableModel
    {
        public IList<GetListTechDto> Items { get; set; }
    }
}
