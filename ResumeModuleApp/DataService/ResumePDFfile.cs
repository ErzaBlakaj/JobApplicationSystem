using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.DependencyInjection;
using ResumeModuleApp.DataService;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ResumeModuleApp.DataService
{
    public interface IResumePdfService
    {
        byte[] GeneratePdf(int resumeId);

    }

    public class ResumePDFfile : IResumePdfService
    {
        private readonly ResumeContext _context;

        public ResumePDFfile(ResumeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public byte[] GeneratePdf(int resumeId)
        {
            var resume = _context.Resumes
                .Include(r => r.Skills)
                .Include(r => r.Experiences)
                .FirstOrDefault(r => r.ResumeId == resumeId);

            if (resume == null)
            {
                // Handle the case where the resume does not exist
                return null;
            }

            // Create a MemoryStream to store the PDF content
            using (var stream = new MemoryStream())
            {
                // Create a PdfWriter instance
                using (var writer = new PdfWriter(stream))
                {
                    // Create a PdfDocument instance
                    using (var pdf = new PdfDocument(writer))
                    {
                        // Create a Document instance
                        var document = new Document(pdf);

                        // Add resume information
                        document.Add(new Paragraph($"Resume ID: {resume.ResumeId}"));
                        document.Add(new Paragraph($"Education: {resume.Education}"));
                        document.Add(new Paragraph($"Position: {resume.Position}"));
                        document.Add(new Paragraph($"Languages: {resume.Languages}"));

                        // Add skills
                        document.Add(new Paragraph("Skills:"));
                        foreach (var skill in resume.Skills)
                        {
                            document.Add(new Paragraph($"- {skill.Description}"));
                        }

                        // Add experiences
                        document.Add(new Paragraph("Experiences:"));
                        foreach (var experience in resume.Experiences)
                        {
                            document.Add(new Paragraph($"- {experience.Description}"));
                        }
                    }
                }

                // Return the PDF content as a byte array
                return stream.ToArray();
            }
        }
    }
}