namespace ResumeModuleApp.DTOs
{
    public class ExperienceDTO
    {
        public int? ExperienceId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Description { get; set; }
        public int? ResumeId { get; set; }
    }
}
