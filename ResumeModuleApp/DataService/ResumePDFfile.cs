﻿using iText.Kernel.Pdf;
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

                        AddUserInformationSection(document, user);

                        AddResumeInformationSection(document, resume);

                        AddSkillsAndExperiencesSection(document, resume);
                    }
                }

                return stream.ToArray();
            }
        }

        private void AddUserInformationSection(Document document, User user)
        {
            document.Add(new Paragraph("\n"));


            document.Add(new Paragraph("User Information").SetFontSize(14).SetBold());

            var userTable = new Table(2).UseAllAvailableWidth();
            userTable.SetBorder(new SolidBorder(1f));
            userTable.AddCell(CreateCell("First Name:").SetBold());
            userTable.AddCell(CreateCell(user.Emri));
            userTable.AddCell(CreateCell("Last Name:").SetBold());
            userTable.AddCell(CreateCell(user.Mbiemri));
            userTable.AddCell(CreateCell("Email:").SetBold());
            userTable.AddCell(CreateCell(user.Email));

            document.Add(userTable);
        }

        private void AddResumeInformationSection(Document document, Resume resume)
        {
            document.Add(new Paragraph("\n"));


            document.Add(new Paragraph("Resume Information").SetFontSize(14).SetBold());

            var infoTable = new Table(2).UseAllAvailableWidth();
            infoTable.SetBorder(new SolidBorder(1f));
            infoTable.AddCell(CreateCell("Education:").SetBold());
            infoTable.AddCell(CreateCell(resume.Education));
            infoTable.AddCell(CreateCell("Position:").SetBold());
            infoTable.AddCell(CreateCell(resume.Position));
            infoTable.AddCell(CreateCell("Languages:").SetBold());
            infoTable.AddCell(CreateCell(resume.Languages));

            document.Add(infoTable);
        }

        private void AddSkillsAndExperiencesSection(Document document, Resume resume)
        {

            document.Add(new Paragraph("\n"));


            document.Add(new Paragraph("Skills and Experiences").SetFontSize(14).SetBold());


            var skillsAndExperiencesTable = new Table(2).UseAllAvailableWidth();
            skillsAndExperiencesTable.SetBorder(new SolidBorder(1f));


            var skillsCell = new Cell().SetBorder(Border.NO_BORDER);
            skillsCell.Add(new Paragraph("Skills").SetBold());
            foreach (var skill in resume.Skills)
            {
                skillsCell.Add(new Paragraph($"- {skill.Description}").SetBorder(Border.NO_BORDER));
            }
            skillsAndExperiencesTable.AddCell(skillsCell);


            var experiencesCell = new Cell().SetBorder(Border.NO_BORDER);
            experiencesCell.Add(new Paragraph("Experiences").SetBold());
            foreach (var experience in resume.Experiences)
            {
                experiencesCell.Add(new Paragraph($"- {experience.Description}").SetBorder(Border.NO_BORDER));
            }
            skillsAndExperiencesTable.AddCell(experiencesCell);

            document.Add(skillsAndExperiencesTable);
        }

        private Cell CreateCell(string text)
        {
            return new Cell().Add(new Paragraph(text)).SetBorder(Border.NO_BORDER);
        }
    }
}