using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private ResumeContext _context;

        public UserController(ResumeContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UsersId }, user);
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.UsersId)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            var existingUser = _context.Users.Find(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Emri = updatedUser.Emri;
            updatedUser.Mbiemri = updatedUser.Mbiemri;
            updatedUser.Email = updatedUser.Mbiemri;
            updatedUser.Password = updatedUser.Password;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }
    }
}