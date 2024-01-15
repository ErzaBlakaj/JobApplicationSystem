using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeModuleApp.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Description { get; set; }
        public int ResumeId { get; set; }

        [ForeignKey("ResumeId")]
        public Resume Resume { get; set; }
    }
}
