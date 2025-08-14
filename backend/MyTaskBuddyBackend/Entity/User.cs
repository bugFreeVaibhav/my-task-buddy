using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskBuddyBackend.Entity
{
    [Table("users")]
    public class User : BaseClass
    {
        

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }   

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }


    }
}
