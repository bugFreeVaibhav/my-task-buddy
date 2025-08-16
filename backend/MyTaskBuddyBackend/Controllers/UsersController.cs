using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTaskBuddyBackend.Dto;
using MyTaskBuddyBackend.Entity;
using MyTaskBuddyBackend.Jwt;


namespace MyTaskBuddyBackend.Controllers
{
    //Add [Authorize] to controllers or endpoints you want to protect:
    [Authorize]
    [Route("[controller]")]
    [ApiController]

    // our UsersController class is inherited From ControllerBase class which is 
    // efficient for REST Api than Controller class which is also inherited From ControllerBase
    // we can use anything based on requirement
    public class UsersController : ControllerBase
    {
        // creating object of AppDbContext class which is used to create connection
        // between application to database 
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public UsersController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _jwtService = new JwtService(config);
        }

        [AllowAnonymous]//To allow access to anyone 
        // register User api
        [HttpPost("register")]

        //we are using IActionResult which supports status codes
        // we are using async to imporve performance when multiple users try to request the 
        // resource or api 
        // Register User
        public async Task<IActionResult> Register([FromBody] UserDto addUserDto)
        {
            if (_context.Users.Any(u => u.Email == addUserDto.Email))
            {
                return BadRequest(new ApiResponse(false, "Email already exists"));
            }

            var user = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password), // ✅ Hash password
                PhoneNumber = addUserDto.PhoneNumber,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(true, "User registered successfully"));
        }


        [AllowAnonymous]
        // Sign In
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto signinDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == signinDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(signinDto.Password, user.Password)) // ✅ Verify hash
            {
                return Unauthorized(new ApiResponse(false, "Invalid credentials"));
            }

            var token = _jwtService.GenerateToken((int)user.Id, user.Email);

            return Ok(new
            {
                success = true,
                message = "Login successful",
                token,
                user = new { user.Id, user.Name, user.Email }
            });
        }



        // Get User By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse (false, "User not found" ));
            }
            return Ok(new ApiDataResponse<User> (true, "Found user",   user ));
        }

        // Update the User
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse (false, "User not found" ));
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new ApiResponse (true, "User updated successfully" ));
        }

        // Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse ( false, "User not found" ));
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new ApiResponse (true, "User deleted successfully" ));
        }

        // Change Password api 
        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse { Success = false, Message = "User not found" });
            }

            if (user.Password != dto.OldPassword)
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Old password is incorrect" });
            }

            user.Password = dto.NewPassword;
            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new ApiResponse { Success = true, Message = "Password changed successfully" });
        }

        // Email Exists
        [HttpGet("check-email/{email}")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == email);
            return Ok(new ApiResponse ( true, "Yes email Exists"));
        }
    }
}
