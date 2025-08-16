using System.ComponentModel.DataAnnotations;

namespace MyTaskBuddyBackend.Dto
{
    public class TaskRespDto
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public long UserId { get; set; }
    }
}
