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
        private readonly IMapper _mapper;

        public ExperienceController(ResumeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddExperience([FromBody] ExperienceDTO experience)
        {
            if (experience == null)
            {
                return BadRequest("Invalid experience data");
            }


            var newExperience = _mapper.Map<Experience>(experience);

            _context.Experiences.Add(newExperience);
            await _context.SaveChangesAsync();

            return Ok();
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
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] ExperienceDTO updatedExperienceDTO)
        {
            if (updatedExperienceDTO == null || id != updatedExperienceDTO.ExperienceId)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            var existingExperience = _context.Experiences.Find(id);

            if (existingExperience == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedExperienceDTO, existingExperience);

            _context.Experiences.Update(existingExperience);
            await _context.SaveChangesAsync();

            return Ok(existingExperience);
        }
    }
}