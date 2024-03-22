using System.Data;
using System.Data.SqlClient;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=contratTest;Trusted_Connection=True;";

        public ClientRepository()
        { 

        }
        public async Task<Client> CreateClientAsync(Client ClientModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Business_area", SqlDbType.VarChar, 50).Value = ClientModel.Business_area;
                    command.Parameters.Add("@Company_name", SqlDbType.VarChar, 50).Value = ClientModel.Company_name;
                    command.Parameters.Add("@Parent_company_name", SqlDbType.VarChar).Value = ClientModel.Parent_company_name;
                    
                    await command.ExecuteNonQueryAsync();
                }
                return ClientModel;
            }
        }

        public Task<Client> DeleteClientASync(int Id_client)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FilterClientASync(string Business_area, string Company_name)
        {
            throw new NotImplementedException();
        }

        public async Task <List<Client>> GetAllClientAsync()
        {
            var clients = new List<Client>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetAllClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var client = new Client();
                            client.Id_client =  reader.GetInt32(0);
                            client.Business_area = reader.GetString(1);
                            client.Company_name = reader.GetString(2);
                            client.Parent_company_name = reader.GetString(3);

                            clients.Add(client);
                        }

                    }
                }
               
            }

            return clients;
        }

        public Task<Client> GetClientByIdAsync(int Id_client)
        {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateClientAsync(int Id_client, Client ClientModel)
        {
            throw new NotImplementedException();
        }
    }
}
