using System.ComponentModel.DataAnnotations;
using TestContrat.Models;

namespace TestContrat.Dtos
{
    public class ExpertDto
    {
        public string name_expert { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string company { get; set; }
        public int Id_expertise { get; set; }
        public Expertise expertise { get; set; }
    }
}
