using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utilities;
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

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<List<UserDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _userService.GetAllAsync();
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var rsp = new Response<SessionDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _userService.ValidateCredentialsAsync(loginDTO.Email, loginDTO.Password);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            var rsp = new Response<UserDTO>();
            try
            {
                if (userDTO.IdRol != Constants.rolAdmin)
                    throw new Exception("Nop!");

                rsp.Status = true;
                rsp.Value = await _userService.CreateAsync(userDTO);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            var rsp = new Response<bool>();
            try
            {
                if (userDTO.IdRol != Constants.rolAdmin)
                    throw new Exception("Nop!");

                rsp.Status = true;
                rsp.Value = await _userService.UpdateAsync(userDTO);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _userService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
