namespace Confab.Modules.Conferences.Core.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class CreateHostDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; }
    }
}