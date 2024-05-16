using System.ComponentModel.DataAnnotations;

namespace TestContrat.Models
{
    public class Directeur
    {
        public int Id_directeur { get; set; }
        [Required]
        [MaxLength(100)]
        public string name_directeur { get; set; }
        [Required]
        [MaxLength(100)]
        public string lastname_directeur { get; set; }
        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        [Required]
        [MaxLength(100)]
        public string telephone { get; set; }
        
    }
}
