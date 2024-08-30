using System.ComponentModel.DataAnnotations;

namespace Ef_code_first.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public ICollection<WorkingExperience> WorkingExperience { get; set; }
    }
}
