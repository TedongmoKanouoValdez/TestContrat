using System.ComponentModel.DataAnnotations;

namespace TestContrat.Models
{
    public class Expert
    {
        public int Id_expert { get; set; }
        [Required]
        [MaxLength(100)]
        public string name_expert { get; set; }
        [Required]
        [MaxLength(100)]
        public string lastname { get; set; }
        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        [Required]
        [MaxLength(100)]
        public string telephone { get; set; }
        [Required]
        [MaxLength(100)]
        public string company { get; set; }
        public int Id_expertise { get; set; }
        [Required]
        [MaxLength(100)]
        public Expertise expertise { get; set; }

    }
}

