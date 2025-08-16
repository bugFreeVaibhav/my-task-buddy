using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskBuddyBackend.Entity
{
    /* this ensures that this class will be not created as table in db */
    [NotMapped]
    public class BaseClass
    {
        [Column(name: "task_Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(name:"created_at")]
        public DateTime CreatedAt { get; set; }


        [Column(name: "updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
