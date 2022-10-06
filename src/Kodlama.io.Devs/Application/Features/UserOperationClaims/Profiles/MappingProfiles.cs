
using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles:Profile
{
	public MappingProfiles()
	{
        CreateMap<UserOperationClaim, UpdatedUserOperationClaimDto>().ReverseMap();
        CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();
        CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();
        CreateMap<IPaginate<UserOperationClaim>, GetListUserOperationClaimModel>().ReverseMap();
        CreateMap<UserOperationClaim, GetListUserOperationClaimDto>().ReverseMap();
        CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
    }
}
