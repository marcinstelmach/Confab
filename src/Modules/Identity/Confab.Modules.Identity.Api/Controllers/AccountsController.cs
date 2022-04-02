namespace Confab.Modules.Identity.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Identity.Core.Dtos;
    using Confab.Modules.Identity.Core.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v1/users")]
    internal class AccountsController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountsController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await _identityService.GetAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] SignUpDto dto)
        {
            await _identityService.SignUpAsync(dto);
            return Accepted();
        }
    }
}