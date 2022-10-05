using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task EmailCannotBeDuplicated(string email)
        {
            User? user = await userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Mail already exists.");
        }
        public async Task UserShouldBeExistWhenRequested(string email)
        {
            User? user = await userRepository.GetAsync(u => u.Email == email);
            if (user == null) throw new BusinessException("User does not exist.");
        }

    }
}
