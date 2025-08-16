using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskBuddyBackend.Entity
{
    [Table("tasks")]
    public class TaskEntity : BaseClass
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public DateTime DueDate { get; set; }

        [StringLength(20)]
        public string Priority { get; set; }

        // Foreign Key
        public long UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
