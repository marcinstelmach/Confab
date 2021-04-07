namespace Confab.Modules.Conferences.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Dtos;
    using Confab.Modules.Conferences.Core.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/hosts")]
    internal class HostsController : ControllerBase
    {
        private readonly IHostsService _hostsService;

        public HostsController(IHostsService hostsService)
        {
            _hostsService = hostsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var hosts = await _hostsService.FindAsync();
            return Ok(hosts);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var host = await _hostsService.GetAsync(id);
            if (host is null)
            {
                return NotFound();
            }

            return Ok(host);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateHostDto dto)
        {
            await _hostsService.AddAsync(dto);
            return Accepted(); // CreateAtRoute || Action
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] HostDto dto)
        {
            dto.Id = id;
            await _hostsService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _hostsService.DeleteAsync(id);
            return NoContent();
        }
    }
}