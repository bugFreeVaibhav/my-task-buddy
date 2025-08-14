using MyTaskBuddyBackend.Entity;
using System.ComponentModel.DataAnnotations;

namespace MyTaskBuddyBackend.Dto
{
    public class AddTaskDto
    {
        [Required]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String Status { get; set; }

        public DateTime DueDate { get; set; }

        public String Priority { get; set; }

        public long UserId { get; set; }
    }
}
