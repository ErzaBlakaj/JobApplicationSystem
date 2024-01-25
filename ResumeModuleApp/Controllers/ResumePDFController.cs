using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;
using Microsoft.AspNetCore.Mvc;
using ResumeModuleApp.DataService;

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

            var pdfContent = _resumePdfService.GeneratePdf(id);

            if (pdfContent == null)
            {
                return NotFound();
            }

            return File(pdfContent, "application/pdf", "Resume.pdf");
        }
    }
}