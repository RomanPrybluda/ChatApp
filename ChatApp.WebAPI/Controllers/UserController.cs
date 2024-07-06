using ChatApp.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById([Required] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync([Required][FromBody] CreateUserDTO request)
        {
            var user = await _userService.CreateUserAsync(request);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([Required] int id, [FromBody] UpdateUserDTO request)
        {
            var user = await _userService.UpdateUserAsync(id, request);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([Required] int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
