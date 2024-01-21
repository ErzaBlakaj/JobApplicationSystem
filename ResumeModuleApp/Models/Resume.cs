using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeModuleApp.Models
{
    public class Resume
    {

        [Key]
        public int ResumeId  { get; set; }
        public string Education { get; set; }
        public string? Position { get; set; }
        public string Languages { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Skills> Skills { get; set; }
        public ICollection<Experience> Experiences { get; set; }
    }
}
