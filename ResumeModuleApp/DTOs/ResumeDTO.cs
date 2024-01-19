namespace ResumeModuleApp.DTOs
{
    public class ResumeDTO
    {
        public string ApplicantName { get; set; }
        public string Education { get; set; }
        public string? Position { get; set; }
        public string Languages { get; set; }
        public int UserId { get; set; }
        public ICollection<SkillsDTO> Skills { get; set; }
        public ICollection<ExperienceDTO> Experiences { get; set; }
    }
}