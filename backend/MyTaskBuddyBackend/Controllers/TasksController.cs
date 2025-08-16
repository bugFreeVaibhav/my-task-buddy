using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTaskBuddyBackend.Dto;
using MyTaskBuddyBackend.Entity;

namespace MyTaskBuddyBackend.Controllers
{
    [Authorize]
    [Route("tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // Add Task
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
            {
                return NotFound(new ApiResponse(false, "User not found"));
            }

            var task = new TaskEntity
            {
                CreatedAt = DateTime.Now,
                Status = dto.Status,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                Title = dto.Title,
                User = user
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(true, "Task created successfully"));
        }

        // Update Task
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] AddTaskDto dto)
        {
            var task = await _context.Tasks.Include(t => t.User)
                                           .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound(new ApiResponse(false, "Task not found"));
            }

            task.Status = dto.Status;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.Priority = dto.Priority;
            task.Title = dto.Title;
            task.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new ApiResponse(true, "Task updated successfully"));
        }

        // Delete Task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound(new ApiResponse(false, "Task not found"));
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(true, "Task deleted successfully"));
        }

        // Get Task by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks.Include(t => t.User)
                                           .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound(new ApiResponse(false, "Task not found"));
            }

            var taskDto = new TaskRespDto
            {
                Id = task.Id,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                Status = task.Status,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Title = task.Title,
                UserId = task.User.Id
            };

            return Ok(taskDto);
        }

        // Get All Tasks
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.Tasks
                                      .Include(t => t.User) // include user details
                                      .ToListAsync();

            var taskDtos = tasks.Select(task => new TaskRespDto
            {
                Id = task.Id,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                Status = task.Status,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Title = task.Title,
                UserId = task.User.Id
            }).ToList();

            return Ok(taskDtos);
        }


    }

}