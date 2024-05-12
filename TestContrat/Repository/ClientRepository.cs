using System.Data;
using System.Data.SqlClient;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=contratTest;Trusted_Connection=True;";
        public async Task<Client> CreateClientAsync(Client ClientModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Code_client", SqlDbType.VarChar, 50).Value = ClientModel.code_client;
                    command.Parameters.Add("@Business_area", SqlDbType.VarChar, 50).Value = ClientModel.business_area;
                    command.Parameters.Add("@Company_name", SqlDbType.VarChar, 50).Value = ClientModel.company_name;
                    command.Parameters.Add("@Parent_company_name", SqlDbType.VarChar).Value = ClientModel.parent_company_name;
                    
                    await command.ExecuteNonQueryAsync();
                }
                return ClientModel;
            }
        }

        public async Task<bool> DeleteClientASync(int Id_client)
        {
            var clientDelet = await GetClientByIdAsync(Id_client);

            if (clientDelet != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteClient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Id_client", Id_client);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
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
                            client.code_client = reader.GetString(1);
                            client.business_area = reader.GetString(2);
                            client.company_name = reader.GetString(3);
                            client.parent_company_name = reader.GetString(4);

                            clients.Add(client);
                        }

                    }
                }
               
            }

            return clients;
        }


        public async Task<Client> GetClientByIdAsync(int Id_client)
        {
            var client = new Client();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetByIdClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id_client", Id_client);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            client = new Client
                            {
                                Id_client = reader.GetInt32(0), // Utilisez GetOrdinal pour obtenir l'index de la colonne
                                code_client = reader.GetString(1),
                                business_area = reader.GetString(2),
                                company_name = reader.GetString(3),
                                parent_company_name = reader.GetString(4)
                            };
                        }
                    }
                }

                return client;
            }
        }

        public async Task<Client> UpdateClientAsync(int Id_client, Client ClientModel)
        {
            var updateClient = await GetClientByIdAsync(Id_client);
            if (updateClient != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("UpdateClient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Code_client", ClientModel.code_client);
                        command.Parameters.AddWithValue("Business_area", ClientModel.business_area);
                        command.Parameters.AddWithValue("Company_name",ClientModel.company_name);
                        command.Parameters.AddWithValue("Parent_company_name", ClientModel.parent_company_name);
                        command.Parameters.AddWithValue("Id_client", Id_client);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            ClientModel.Id_client = Id_client;
            return ClientModel;

        }
    }
}
