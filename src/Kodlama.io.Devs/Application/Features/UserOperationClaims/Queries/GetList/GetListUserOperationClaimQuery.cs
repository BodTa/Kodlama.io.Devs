

using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQuery:IRequest<GetListUserOperationClaimModel>,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles { get; } = { "admin" };

}
public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, GetListUserOperationClaimModel>
{
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public GetListUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
    }
    async Task<GetListUserOperationClaimModel> IRequestHandler<GetListUserOperationClaimQuery, GetListUserOperationClaimModel>.Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
    {
        IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

        var mappedUserOperationClaims = _mapper.Map<GetListUserOperationClaimModel>(userOperationClaims);
        return mappedUserOperationClaims;
    }
}
