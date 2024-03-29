﻿using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Microsoft.Extensions.DependencyInjection;
using ResumeModuleApp.DataService;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.DataService
{
    public interface IResumePdfBuilder
    {
        void BuildUserInformationSection(User user);
        void BuildResumeInformationSection(Resume resume);
        void BuildSkillsAndExperiencesSection(Resume resume);
    }

    public class ResumePdfBuilder : IResumePdfBuilder
    {
        private Document _document;

        public ResumePdfBuilder(Document document)
        {
            _document = document;
        }

        public void BuildUserInformationSection(User user)
        {
            _document.Add(new Paragraph("\n"));

            var titleStyle = new Style()
                .SetFontSize(14)
                .SetBold();

            var titleParagraph = new Paragraph("User Information")
                .AddStyle(titleStyle)
                .SetBackgroundColor(DeviceRgb.RED);

            _document.Add(titleParagraph);

            var userTable = new Table(2).UseAllAvailableWidth();
            userTable.SetBorder(new SolidBorder(1f));
            userTable.AddCell(CreateCell("First Name:").AddStyle(titleStyle));
            userTable.AddCell(CreateCell(user.Emri));
            userTable.AddCell(CreateCell("Last Name:").AddStyle(titleStyle));
            userTable.AddCell(CreateCell(user.Mbiemri));
            userTable.AddCell(CreateCell("Email:").AddStyle(titleStyle));
            userTable.AddCell(CreateCell(user.Email));

            _document.Add(userTable);
        }

        public void BuildResumeInformationSection(Resume resume)
        {
            _document.Add(new Paragraph("\n"));

            var titleStyle = new Style()
                .SetFontSize(14)
                .SetBold();

            var titleParagraph = new Paragraph("Resume Information")
                .AddStyle(titleStyle)
                .SetBackgroundColor(DeviceRgb.RED);

            _document.Add(titleParagraph);

            var infoTable = new Table(2).UseAllAvailableWidth();
            infoTable.SetBorder(new SolidBorder(1f));
            infoTable.AddCell(CreateCell("Education:").AddStyle(titleStyle));
            infoTable.AddCell(CreateCell(resume.Education));
            infoTable.AddCell(CreateCell("Position:").AddStyle(titleStyle));
            infoTable.AddCell(CreateCell(resume.Position));
            infoTable.AddCell(CreateCell("Languages:").AddStyle(titleStyle));
            infoTable.AddCell(CreateCell(resume.Languages));

            _document.Add(infoTable);
        }

        public void BuildSkillsAndExperiencesSection(Resume resume)
        {
            _document.Add(new Paragraph("\n"));

            var titleStyle = new Style()
                .SetFontSize(14)
                .SetBold();

            var titleParagraph = new Paragraph("Skills and Experiences")
                .AddStyle(titleStyle)
                .SetBackgroundColor(DeviceRgb.RED);

            _document.Add(titleParagraph);

            var skillsAndExperiencesTable = new Table(2).UseAllAvailableWidth();
            skillsAndExperiencesTable.SetBorder(new SolidBorder(1f));

            var skillsCell = new Cell().SetBorder(Border.NO_BORDER);
            skillsCell.Add(new Paragraph("Skills").AddStyle(titleStyle));
            foreach (var skill in resume.Skills)
            {
                skillsCell.Add(new Paragraph($"- {skill.Description}").SetBorder(Border.NO_BORDER));
            }
            skillsAndExperiencesTable.AddCell(skillsCell);

            var experiencesCell = new Cell().SetBorder(Border.NO_BORDER);
            experiencesCell.Add(new Paragraph("Experiences").AddStyle(titleStyle));
            foreach (var experience in resume.Experiences)
            {
                experiencesCell.Add(new Paragraph($"- {experience.Description}").SetBorder(Border.NO_BORDER));
            }
            skillsAndExperiencesTable.AddCell(experiencesCell);

            _document.Add(skillsAndExperiencesTable);
        }

        private Cell CreateCell(string text)
        {
            return new Cell().Add(new Paragraph(text)).SetBorder(Border.NO_BORDER);
        }
    }

    public interface IResumePdfService
    {
        byte[] GeneratePdf(int resumeId, User user);
    }

    public class ResumePDFfile : IResumePdfService
    {
        private readonly ResumeContext _context;

        public ResumePDFfile(ResumeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public byte[] GeneratePdf(int resumeId, User user = null)
        {
            var resume = _context.Resumes
                .Include(r => r.Skills)
                .Include(r => r.Experiences)
                .FirstOrDefault(r => r.ResumeId == resumeId);

            if (resume == null || user == null)
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

                        document.Add(new Paragraph("Resume").SetFontSize(18).SetBold());

                        var builder = new ResumePdfBuilder(document);
                        builder.BuildUserInformationSection(user);
                        builder.BuildResumeInformationSection(resume);
                        builder.BuildSkillsAndExperiencesSection(resume);
                    }
                }

                return stream.ToArray();
            }
        }
    }
}
