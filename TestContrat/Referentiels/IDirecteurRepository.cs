using TestContrat.Models;

namespace TestContrat.Referentiels
{
    public interface IDirecteurRepository
    {
        Task<Directeur> CreateDirecteurAsync(Directeur DirecteurModel);
        Task<List<Directeur>> GetAllDirecteurAsync();
        Task<Directeur> GetDirecteurByIdAsync(int Id_directeur);
        Task<bool> DeleteDirecteurAsync(int Id_directeur);
        Task<Directeur> UpdateDirecteurAsync(int Id_directeur, Directeur DirecteurModel);
    }
}
