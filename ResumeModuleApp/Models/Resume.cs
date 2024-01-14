using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeModuleApp.Models
{
    public class Resume
    {
        [Key]
        public int ResumeId  { get; set; }
        public string ApplicantName { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string? Position { get; set; }
    }
}
