using AdministratioSchool.Domain.DTO.In;
using AdministratioSchool.Domain.DTO.Out;
using AdministratioSchool.Domain.Entities;
using AdministratioSchool.Domain.Services;
using AdministratioSchool.Infraestructure.Persistence.Contex;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministratioSchool.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public UserController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userService = new UserService(_context, _mapper);
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserInDTO userInDTO)
        {
            try
            {
                var user = await _userService.CreateUser(userInDTO);
                CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
                return Ok(new { success = true, message = "User created successfully", user });

            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<UserOutDTO>> GetUsers()
        {
            try
            {
                var userOutDTOs = await _userService.GetUsers();

                return Ok(new { success = true, message = "Users retrived successfully", userOutDTOs });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(new { success = true, message = "User retrieved successfully", user });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserById(int id)
        {
            try
            {
                var user = await _userService.DeleteUser(id);
                return Ok(new { success = true, message = "User removed successfully", user });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUserById(int id, [FromBody] UserInDTO userInDTO)
        {

            try
            {
                var updatedUser = await _userService.UpdateUser(id, userInDTO);
                return Ok(new { success = true, message = "User updated successfully", updatedUser });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}

