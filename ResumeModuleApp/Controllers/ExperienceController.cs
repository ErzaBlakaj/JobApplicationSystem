using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using ResumeModuleApp.DTOs;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExperienceController : Controller
    {

        private ResumeContext _context;

        public ExperienceController(ResumeContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddExperience([FromBody] Experience experience)
        {
            if (experience == null)
            {
                return BadRequest("Invalid experience data");
            }

            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExperience), new { id = experience.ExperienceId }, experience);
        }

        [HttpGet("{id}")]
        public IActionResult GetExperience(int id)
        {
            var experience = _context.Experiences.Find(id);



            if (experience == null)
            {
                return NotFound();
            }

            return Ok(experience);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var experience = _context.Experiences.Find(id);

            if (experience == null)
            {
                return NotFound();
            }

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] Experience updatedExperience)
        {
            if (updatedExperience == null || id != updatedExperience.ExperienceId)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            var existingExperience = _context.Experiences.Find(id);

            if (existingExperience == null)
            {
                return NotFound();
            }

            updatedExperience.DateFrom = updatedExperience.DateFrom;
            updatedExperience.DateTo = updatedExperience.DateTo;
            updatedExperience.Description = updatedExperience.Description;

            _context.Experiences.Update(existingExperience);
            await _context.SaveChangesAsync();

            return Ok(existingExperience);
        }
    }
}