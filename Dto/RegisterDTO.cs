using System.ComponentModel.DataAnnotations;

namespace NetworkingWebApp.Dto
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Password
        {
            get; set;
        }
    }
}
