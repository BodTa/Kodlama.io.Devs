using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.UserLogin;

public class UserLoginQuery:UserForLoginDto,IRequest<AccessToken>
{
}
public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, AccessToken>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private readonly ITokenHelper tokenHelper;

    public UserLoginQueryHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.tokenHelper = tokenHelper;
    }
    public async Task<AccessToken> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
         User user = await userRepository.GetAsync(u => u.Email.ToLower() == request.Email.ToLower(),
            include: u => u.Include(u => u.UserOperationClaims).ThenInclude(o => o.OperationClaim));
        if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new BusinessException("Given Email or Password wrong.");
        }
        List<OperationClaim> operationClaims = new List<OperationClaim>();
        foreach(var operationClaim in user.UserOperationClaims)
        {
            operationClaims.Add(operationClaim.OperationClaim);
        }
        var token = tokenHelper.CreateToken(user,operationClaims);
        return token;
    }
}
