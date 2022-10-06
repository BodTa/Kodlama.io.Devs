

using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommand:IRequest<CreatedUserOperationClaimDto>
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}
public class CreateUserOperationClaimCommandHanlder : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public CreateUserOperationClaimCommandHanlder(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var mappedRequest = _mapper.Map<UserOperationClaim>(request);
        UserOperationClaim createdUserOperationClaim =await _userOperationClaimRepository.AddAsync(mappedRequest);
        var mappedCreatedClaim = _mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);
        return mappedCreatedClaim;
    }
}
