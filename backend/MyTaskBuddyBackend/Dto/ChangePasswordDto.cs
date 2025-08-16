using System.ComponentModel.DataAnnotations;

namespace MyTaskBuddyBackend.Dto
{
    public class ChangePasswordDto
    {

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string NewPassword { get; set; }
    }
}
