using System.ComponentModel.DataAnnotations;

namespace Ef_code_first.Models
{
    public class WorkingExperience
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Details {  get; set; }
        public string Environment { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
