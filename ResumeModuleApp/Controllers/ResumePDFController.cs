using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using Microsoft.EntityFrameworkCore;

namespace ResumeModuleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumePDFController : Controller
    {
        private readonly ResumeContext _context;
        private readonly IResumePdfService _resumePdfService;

        public ResumePDFController(ResumeContext context, IResumePdfService resumePdfService)
        {
            _context = context;
            _resumePdfService = resumePdfService;
        }

        [HttpGet("pdf/{id}")]
        public IActionResult GetResumePdf(int id)
        {
            var resume = _context.Resumes
                .Include(r => r.User)
                .Include(r => r.Skills)
                .Include(r => r.Experiences)
                .FirstOrDefault(r => r.ResumeId == id);

            if (resume == null)
            {
                return NotFound("Resume not found");
            }

            var user = resume.User;

            if (user == null)
            {
                return NotFound("User not found");
            }

            var pdfContent = _resumePdfService.GeneratePdf(id, user);

            if (pdfContent == null)
            {
                return NotFound("PDF generation failed");
            }

            return File(pdfContent, "application/pdf", $"Resume_{id}.pdf");
        }
    }
}

