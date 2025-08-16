using System.ComponentModel.DataAnnotations;

namespace MyTaskBuddyBackend.Dto
{
    public class UserDto
    {
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

    }
}
