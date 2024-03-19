using System.Collections.Generic;
using TestContrat.Models;

namespace TestContrat.Referentiels
{
    public interface IFamillyRepository
    {
        Task<Familly> CreateAsync(Familly famillyModels);
        Task<List<Familly>> GetAllAsync();
        Task<Familly> GetByIdAsync(int id);
        Task<Familly> DeleteAsync(int id);

        Task<Familly> UpdateAsync(int id,Familly famillyModels);
    }
}
