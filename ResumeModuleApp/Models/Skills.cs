using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeModuleApp.Models
{
    public class Skills
    {
        [Key]
        public int SkillsId { get; set; }
        public string Description { get; set; }
        public int ResumeId { get; set; }

        [ForeignKey("ResumeId")]
        public Resume Resume { get; set; }
    }
}
