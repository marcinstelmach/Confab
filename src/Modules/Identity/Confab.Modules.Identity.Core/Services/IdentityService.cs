using System.Collections.Generic;
using System.Text.Json;

namespace Confab.Modules.Identity.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Identity.Core.Dtos;
    using Confab.Modules.Identity.Core.Entities;
    using Confab.Modules.Identity.Core.Exceptions;
    using Confab.Shared.Abstractions.Auth;
    using Microsoft.AspNetCore.Identity;

    internal class IdentityService : IIdentityService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IAuthManager _authManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public IdentityService(IUsersRepository usersRepository, IAuthManager authManager, IPasswordHasher<User> passwordHasher)
        {
            _usersRepository = usersRepository;
            _authManager = authManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _usersRepository.GetAsync(id);
            if (user is null)
            {
                throw new UserNotFoundException(id);
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                CreationDateTime = user.CreationDateTime,
                Role = user.Role,
                Claims = user.Claims
            };
        }

        public async Task<JsonWebToken> SignInAsync(SignInDto dto)
        {
            var user = await _usersRepository.GetAsync(dto.Email);
            if (user is null || _passwordHasher.VerifyHashedPassword(default, user.Password, dto.Password) is not PasswordVerificationResult.Success)
            {
                throw new InvalidUserEmailOrPasswordException();
            }

            if (!user.IsActive)
            {
                throw new UserNotActiveException(user.Id);
            }

            return _authManager.GenerateToken(user.Id, user.Email, user.Role, user.Claims);
        }

        public async Task SignUpAsync(SignUpDto dto)
        {
            if (await _usersRepository.ExistsAsync(dto.Email))
            {
                throw new UserWithGivenEmailAlreadyExistsException(dto.Email);
            }

            var hashedPassword = _passwordHasher.HashPassword(default, dto.Password);
            var user = new User(dto.Email, hashedPassword, dto.Role, dto.Claims);
            _usersRepository.Add(user);
            await _usersRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}