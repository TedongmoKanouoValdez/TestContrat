using TestContrat.Models;

namespace TestContrat.Referentiels
{
    public interface IExpertiseRepository
    {
        Task<Expertise> CreateExpertiseAsync(Expertise expertiseModel);
        Task<Expertise> UpdateExpertiseAsync(int Id_expertise, Expertise expertiseModel);
        Task<bool> DeleteExpertiseAsync(int Id_expertise);
        Task<Expertise> GetExpertiseByIdAsync(int Id_expertise);
        Task<List<Expertise>> GetAllExpertiseAsync();
    }
}
