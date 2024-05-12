using System.ComponentModel.DataAnnotations;

namespace TestContrat.Models
{
    public class Expertise
    {
        public int Id_expertise { get; set; }
        [Required]
        [MaxLength(100)]
        public string name_expertise { get; set; }
    }
}
