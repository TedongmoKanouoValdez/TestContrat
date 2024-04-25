using TestContrat.Models;

namespace TestContrat.Referentiels
{
    public interface IAdministrateurRepository
    {
        Task<Administrateur> CreateAdminAsync(Administrateur adminModel);
        Task<Administrateur> UpdateAdminAsync(int IdAdmin,Administrateur adminModel);
        Task<bool> DeleteAdminAsync(int IdAdmin);
        Task<Administrateur> GetAdminByIdAsync(int IdAdmin);
        Task<List<Administrateur>> GetAllAdminsAsync();
    }
}
