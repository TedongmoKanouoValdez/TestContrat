using System.ComponentModel.DataAnnotations;

namespace TestContrat.Dtos
{
    public class DirecteurDto
    {
        public int Id_directeur { get; set; }
        public string name_directeur { get; set; }
        public string lastname_directeur { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
    }
}
