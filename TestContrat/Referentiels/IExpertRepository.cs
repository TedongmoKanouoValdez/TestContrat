using TestContrat.Models;

namespace TestContrat.Referentiels
{
    public interface IExpertRepository
    {
        Task<Expert> CreateExpertAsync(Expert expertModel);
        Task<Expert> UpdateExpertAsync(int Id_expert, Expert expertModel);
        Task<bool> DeleteExpertAsync(int Id_expert);
        Task<Expert> GetExpertByIdAsync(int Id_expert);
        Task<List<Expert>> GetAllExpertAsync();
        Task<List<Expertise>> GetAllExpertisesAsync();
    }
}
