using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskBuddyBackend.Entity
{

    [Table("tasks")]
    public class Task:BaseClass
    {
        [Required]
        [StringLength(50)]
        public String Title { get; set; }
        [Required]
        [StringLength(200)]
        public String Description { get; set; }
        [Required]
        [StringLength(20)]
        public String Status { get; set; }

        public DateTime DueDate { get; set; }

        public String Priority { get; set; }

        public User User { get; set; }

    }
}
