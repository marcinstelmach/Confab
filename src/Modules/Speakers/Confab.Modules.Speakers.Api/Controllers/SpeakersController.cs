namespace Confab.Modules.Speakers.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Speakers.Core.Dtos;
    using Confab.Modules.Speakers.Core.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/speakers")]
    internal class SpeakersController : ControllerBase
    {
        private readonly ISpeakersService _speakersService;

        public SpeakersController(ISpeakersService speakersService)
        {
            _speakersService = speakersService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var speaker = await _speakersService.GetAsync(id);
            return Ok(speaker);
        }

        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var speakers = await _speakersService.FindAsync();
            return Ok(speakers);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]AddSpeakerDto speaker)
        {
            await _speakersService.AddAsync(speaker);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _speakersService.DeleteAsync(id);
            return NoContent();
        }
    }
}