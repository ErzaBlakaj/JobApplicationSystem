using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using ResumeModuleApp.DTOs;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : Controller
    {

        private ResumeContext _context;
        private readonly IMapper _mapper;

        public SkillsController(ResumeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddSkills([FromBody] SkillsDTO skills)
        {
            if (skills == null)
            {
                return BadRequest("Invalid skills data");
            }

            if (_context.Skills.Any(s => s.Description == skills.Description && s.ResumeId == skills.ResumeId))
            {
                return Conflict("The skill with this description already exists");
            }

            var newSkills = _mapper.Map<Skills>(skills);

            _context.Skills.Add(newSkills);
            await _context.SaveChangesAsync();


            return Ok();
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
        public async Task<IActionResult> UpdateSkills(int id, [FromBody] SkillsDTO updatedSkillsDTO)
        {
            if (updatedSkillsDTO == null || id != updatedSkillsDTO.SkillsId)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            var existingSkills = _context.Skills.Find(id);

            if (existingSkills == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedSkillsDTO, existingSkills);

       

            _context.Skills.Update(existingSkills);
            await _context.SaveChangesAsync();

            return Ok(existingSkills);
        }

    }
}
