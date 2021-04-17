namespace Confab.Modules.Speakers.Core.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class AddSpeakerDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 150)]
        public int Age { get; set; }
    }
}