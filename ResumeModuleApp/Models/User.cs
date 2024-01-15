using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeModuleApp.Models
{
    public class User
    {
        [Key]
        public int UsersId { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Resume> Resumes { get; set; }
    }
}
