using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Common;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.Utility;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Return all the users, Ok-status code</returns>
        //[ServiceFilter(typeof(CustomAuthorizationFilter))]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(new Response<List<UserDTO>>
            {
                Success = true,
                Value = users
            });
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
                throw new BadRequestException("The Email and Password can not be empty.");

            var session = await _userService.ValidateCredentialsAsync(loginDTO.Email, loginDTO.Password);

            return Ok(new Response<SessionDTO>
            {
                Success = true,
                Value = session
            });
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="userDTO" <see cref="UserDTO"/>></param>
        /// <returns>201 Created</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            if (userDTO.IdRol != Constants.rolAdmin)
                throw new UnauthorizedAccessException("Nop!");

            var createdUser = await _userService.CreateAsync(userDTO);

            return Created(
                uri: Url.Action(nameof(Create), new { id = createdUser.IdUser }),
                value: new Response<UserDTO>
                {
                    Success = true,
                    Value = createdUser
                }
            );
        }

        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="userDTO" <see cref="UserDTO"/>></param>
        /// <returns>204-no content</returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            if (userDTO.IdRol != Constants.rolAdmin)
                throw new UnauthorizedAccessException("Nop!");

            var updated = await _userService.UpdateAsync(userDTO);

            return Ok(new Response<bool>
            {
                Success = true,
                Value = updated
            });
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);

            return Ok(new Response<bool>
            {
                Success = true,
                Value = deleted
            });
        }
    }
}
