using System.ComponentModel.DataAnnotations;

namespace TestContrat.Models
{
    public class Administrateur
    {
        public int IdAdmin { get; set; }
        [Required]
        [MaxLength(100)]
        public string firstname { get; set; }
        [Required]
        [MaxLength(100)]
        public string lastname { get; set; }
        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        [Required]
        [MaxLength(100)]
        public string phoneNumber { get; set; }
    }
}
