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
    }
}
