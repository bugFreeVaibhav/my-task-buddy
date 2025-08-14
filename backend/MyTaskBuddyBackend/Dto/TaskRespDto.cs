using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskBuddyBackend.Dto
{
    public class TaskRespDto
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
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
