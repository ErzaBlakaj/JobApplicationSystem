﻿using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Document = iText.Layout.Document;
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
               
                return null;
            }

         
            using (var stream = new MemoryStream())
            {
          
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                       
                        document.Add(new Paragraph($"Resume ID: {resume.ResumeId}"));
                        document.Add(new Paragraph($"Education: {resume.Education}"));
                        document.Add(new Paragraph($"Position: {resume.Position}"));
                        document.Add(new Paragraph($"Languages: {resume.Languages}"));

                       
                        document.Add(new Paragraph("Skills:"));
                        foreach (var skill in resume.Skills)
                        {
                            document.Add(new Paragraph($"- {skill.Description}"));
                        }

                      
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