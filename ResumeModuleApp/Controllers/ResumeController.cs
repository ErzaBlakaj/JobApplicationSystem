using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using ResumeModuleApp.DTOs;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumeController : Controller
    {

        private ResumeContext _context;
        private readonly IMapper _mapper;

        public ResumeController(ResumeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddResume([FromBody] ResumeDTO resume)
        {
            if (resume == null)
            {
                return BadRequest("Invalid resume data");
            }

            //if( _context.Resumes.Any(r => r.ApplicantName == resume.ApplicantName))
            //{
            //    return Conflict("Resume with this name already exists");
            //}

            var newResume = _mapper.Map<Resume>(resume);

            _context.Resumes.Add(newResume);
            await _context.SaveChangesAsync();

            return Ok();

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

            //existingResume.ResumeId = updatedResume.ResumeId;
            existingResume.ApplicantName = updatedResume.ApplicantName;
            existingResume.Education = updatedResume.Education;
            existingResume.Position = updatedResume.Position;

            _context.Resumes.Update(existingResume);
            await _context.SaveChangesAsync();

            return Ok(existingResume);
        }
    }
}
