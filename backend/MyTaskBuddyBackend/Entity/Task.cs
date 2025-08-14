using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskBuddyBackend.Entity
{

    [Table("tasks")]
    public class Task
    {
        [Column(name: "task_Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TaskId { set; get; }
        public String Title { get; set; }
        public String Description { get; set; }

        public String Status { get; set; }

        public DateTime DueDate { get; set; }

        public String Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }

    }
}
