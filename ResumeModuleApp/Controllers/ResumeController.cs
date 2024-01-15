using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumeController : Controller
    {

        private ResumeContext _context;

        public ResumeController(ResumeContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddResume([FromBody] Resume resume)
        {
            if (resume == null)
            {
                return BadRequest("Invalid resume data");
            }

            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResume), new { id = resume.ResumeId }, resume);
        }

        [HttpGet("{id}")]
        public IActionResult GetResume(int id)
        {
            var resume = _context.Resumes.Find(id);



            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteResume(int id)
        {
            var resume = _context.Resumes.Find(id);

            if (resume == null)
            {
                return NotFound();
            }

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateResume(int id, [FromBody] Resume updatedResume)
        {
            if (updatedResume == null || id != updatedResume.ResumeId)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            var existingResume = _context.Resumes.Find(id);

            if (existingResume == null)
            {
                return NotFound();
            }

            existingResume.ResumeId = updatedResume.ResumeId;
            existingResume.ApplicantName = updatedResume.ApplicantName;
            existingResume.Education = updatedResume.Education;
            existingResume.Experience = updatedResume.Experience;
            existingResume.Position = updatedResume.Position;
            existingResume.skills = updatedResume.skills;

            _context.Resumes.Update(existingResume);
            await _context.SaveChangesAsync();

            return Ok(existingResume);
        }
    }
}
