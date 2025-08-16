using System.ComponentModel.DataAnnotations;

namespace MyTaskBuddyBackend.Dto
{
    public class SignInDto
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
