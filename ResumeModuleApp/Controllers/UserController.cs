using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using ResumeModuleApp.DTOs;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private ResumeContext _context;
        private readonly IMapper _mapper;

        public UserController(ResumeContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }

            //User newUser = new User
            //{
            //    Emri = user.Emri,
            //    Email = user.Email,
            //    Mbiemri = user.Mbiemri,
            //    Password = user.Password
            //};

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("User with this email already exists");
            }

            var newUser = _mapper.Map<User>(user);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // You can return the created user or a success message based on your requirements
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);



            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO updatedUserDTO)
        {
            if (updatedUserDTO == null || id != updatedUserDTO.UsersId)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            //var newUser = _mapper.Map<User>(updatedUserDTO);
            _mapper.Map(updatedUserDTO, existingUser);

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }

    }
}