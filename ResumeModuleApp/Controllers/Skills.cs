using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : Controller
    {

        private ResumeContext _context;

        public SkillsController(ResumeContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddSkills([FromBody] Skills skills)
        {
            if (skills == null)
            {
                return BadRequest("Invalid skills data");
            }

            _context.Skills.Add(skills);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkills), new { id =skills.SkillsId }, skills);
        }

        [HttpGet("{id}")]
        public IActionResult GetSkills(int id)
        {
            var skills = _context.Skills.Find(id);



            if (skills == null)
            {
                return NotFound();
            }

            return Ok(skills);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSkills(int id)
        {
            var skills = _context.Skills.Find(id);

            if (skills == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skills);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSkills(int id, [FromBody] Skills updatedSkills)
        {
            if (updatedSkills== null || id != updatedSkills.SkillsId)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            var existingSkills = _context.Skills.Find(id);

            if (existingSkills == null)
            {
                return NotFound();
            }

          
          
            existingSkills.Description = updatedSkills.Description;

            _context.Skills.Update(existingSkills);
            await _context.SaveChangesAsync();

            return Ok(existingSkills);
        }
    }
}
