namespace Confab.Modules.Conferences.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Dtos;
    using Confab.Modules.Conferences.Core.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/conferences")]
    internal class ConferencesController : ControllerBase
    {
        private readonly IConferencesService _conferencesService;

        public ConferencesController(IConferencesService conferencesService)
        {
            _conferencesService = conferencesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var dtos = await _conferencesService.FindAsync();
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var dto = await _conferencesService.GetAsync(id);
            if (dto is null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateConferenceDto dto)
        {
            await _conferencesService.AddAsync(dto);
            return Accepted(); // CreateAtRoute || Action
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ConferenceDto dto)
        {
            dto.Id = id;
            await _conferencesService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _conferencesService.DeleteAsync(id);
            return NoContent();
        }
    }
}