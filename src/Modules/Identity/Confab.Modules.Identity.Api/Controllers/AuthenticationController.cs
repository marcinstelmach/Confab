namespace Confab.Modules.Identity.Api.Controllers
{
    using System.Threading.Tasks;
    using Confab.Modules.Identity.Core.Dtos;
    using Confab.Modules.Identity.Core.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/authentication")]
    internal class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthenticationController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] SignInDto dto)
        {
            var token = await _identityService.SignInAsync(dto);
            return Ok(token);
        }
    }
}