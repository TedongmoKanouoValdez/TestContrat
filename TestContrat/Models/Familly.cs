using System.ComponentModel.DataAnnotations;

namespace TestContrat.Models
{
    public class Familly
    {
        public int Id { get;  set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
    }
}
