namespace Confab.Modules.Identity.Core.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SignUpDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}