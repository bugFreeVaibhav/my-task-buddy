using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskBuddyBackend.Dto;
using MyTaskBuddyBackend.Entity;
namespace MyTaskBuddyBackend.Controllers
{
    [Route("tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        AppDbContext context = new AppDbContext();

        [HttpPost]
        public ApiResponse AddTask([FromBody] AddTaskDto t1)
        {
            Entity.Task t2 = new Entity.Task();
            t2.CreatedAt = DateTime.Now;
            t2.Status = t1.Status;
            t2.Description = t1.Description;
            t2.DueDate = t1.DueDate;
            t2.Priority = t1.Priority;
            t2.Title = t1.Title;

            //fetch user By Id ;
            User u1 = new User();
            t2.User = u1;
            context.Tasks.Add(t2);
            context.SaveChanges();
            return new ApiResponse(true);
        }

        //if Path variable is taskId then in parameter 
        //[FromRoute] int id  -->write [FromRoute] as taskId and id name differnent
        [HttpPut("{id}")]
        public ApiResponse UpdateTask(int id,[FromBody] AddTaskDto t1)
        {
            //Finding  Task By Id 
            Entity.Task t2=context.Tasks.Find(id);
            if (t2 == null)
            {
                return new ApiResponse(false);
            }
            else
            {
                //t2.CreatedAt = DateTime.Now; not needed as already created 
                t2.Status = t1.Status;
                t2.Description = t1.Description;
                t2.DueDate = t1.DueDate;
                t2.Priority = t1.Priority;
                t2.Title = t1.Title;
                t2.UpdatedAt = DateTime.Now;
                //fetch user By Id ;
                User u1 = new User();
                t2.User = u1;
                //context.Tasks.Add(t2);
                context.SaveChanges();
                return new ApiResponse(true);
            }
        }


        [HttpDelete("{id}")]
        public ApiResponse DeleteTask(int id)
        {
            Entity.Task t1 = context.Tasks.Find(id);
            if (t1 != null)
            {
                context.Tasks.Remove(t1);
            }
            else
            {
                return new ApiResponse(false);
            }
            return new ApiResponse(true);
        }

        [HttpGet("{id}")]
        public TaskRespDto getTaskById(int id)
        {
            Entity.Task t1=context.Tasks.Find(id);
            TaskRespDto t2 = new TaskRespDto();
            t2.CreatedAt = DateTime.Now;
            t2.Status = t1.Status;
            t2.Description = t1.Description;
            t2.DueDate = t1.DueDate;
            t2.Priority = t1.Priority;
            t2.Title = t1.Title;
            t2.Id = t1.Id;
            t2.UpdatedAt = t1.UpdatedAt;
            t2.UserId = t1.User.Id;
            return t2;
        }
    }
}
