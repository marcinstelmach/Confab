namespace Confab.Modules.Identity.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Identity.Core.Dtos;
    using Confab.Shared.Abstractions.Auth;

    internal interface IIdentityService
    {
        Task<UserDto> GetAsync(Guid id);

        Task<JsonWebToken> SignInAsync(SignInDto dto);

        Task SignUpAsync(SignUpDto dto);
    }
}