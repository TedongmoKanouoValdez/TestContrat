using TestContrat.Models;

namespace TestContrat.Referentiels
{
    public interface IClientRepository
    {
        Task<Client> CreateClientAsync(Client ClientModel);
        Task<List<Client>> GetAllClientAsync();
        Task<Client> GetClientByIdAsync(int Id_client);
        Task<Client> DeleteClientASync(int Id_client);
        Task<Client> UpdateClientAsync(int Id_client, Client ClientModel);
        Task<Client> FilterClientASync(string Business_area, string Company_name);
    }
}
