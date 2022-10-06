
using Application.Features.OperationClaims.Models.GetListModel;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery:IRequest<GetListOperationClaimModel>
{
    public PageRequest PageRequest { get; set; }
}

public class GetListOperationClaimQueryHanlder : IRequestHandler<GetListOperationClaimQuery, GetListOperationClaimModel>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;

    public GetListOperationClaimQueryHanlder(IMapper mapper, IOperationClaimRepository operationClaimRepository)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<GetListOperationClaimModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
    {
        IPaginate<OperationClaim> OperationClaims = await _operationClaimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
        var mappedOperationClaims = _mapper.Map<GetListOperationClaimModel>(OperationClaims);
        return mappedOperationClaims;
    }
}