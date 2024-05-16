using System.Data.SqlClient;
using System.Data;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Repository
{
    public class DirecteurRepository : IDirecteurRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=contratTest;Trusted_Connection=True;";

        public async Task<Directeur> CreateDirecteurAsync(Directeur DirecteurModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddDirecteur", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Name_directeur", SqlDbType.VarChar, 50).Value = DirecteurModel.name_directeur;
                    command.Parameters.Add("@Lastname_directeur", SqlDbType.VarChar, 50).Value = DirecteurModel.lastname_directeur;
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DirecteurModel.email;
                    command.Parameters.Add("@Telephone", SqlDbType.VarChar).Value = DirecteurModel.telephone;

                    await command.ExecuteNonQueryAsync();
                }
                return DirecteurModel;
            }
        }

        public async Task<bool> DeleteDirecteurAsync(int Id_directeur)
        {
            var directeurDelete = await GetDirecteurByIdAsync(Id_directeur);

            if (directeurDelete != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteDirecteur", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Id_directeur", Id_directeur);

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

        public async Task<List<Directeur>> GetAllDirecteurAsync()
        {
            var directeurs = new List<Directeur>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetAllDirecteur", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var directeur = new Directeur();
                            directeur.Id_directeur = reader.GetInt32(0);
                            directeur.name_directeur = reader.GetString(1);
                            directeur.lastname_directeur = reader.GetString(2);
                            directeur.email = reader.GetString(3);
                            directeur.telephone = reader.GetString(4);

                            directeurs.Add(directeur);
                        }

                    }
                }

            }

            return directeurs;
        }

        public async Task<Directeur> GetDirecteurByIdAsync(int Id_directeur)
        {
            var directeur = new Directeur();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetByIdDirecteur", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id_directeur", Id_directeur);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            directeur = new Directeur
                            {
                                Id_directeur = reader.GetInt32(0),
                                name_directeur = reader.GetString(1),
                                lastname_directeur = reader.GetString(2),
                                email = reader.GetString(3),
                                telephone = reader.GetString(4),
                                
                            };

                        }
                    }
                }
                return directeur;
            }
        }

        public async Task<Directeur> UpdateDirecteurAsync(int Id_directeur, Directeur directeurModel)
        {
            var updateDirecteur = await GetDirecteurByIdAsync(Id_directeur);
            if (updateDirecteur != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("UpdateDirecteur", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@name_directeur", SqlDbType.VarChar, 50).Value = directeurModel.name_directeur;
                        command.Parameters.Add("@lastname_directeur", SqlDbType.VarChar, 50).Value = directeurModel.lastname_directeur;
                        command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = directeurModel.email;
                        command.Parameters.Add("@telephone", SqlDbType.VarChar, 50).Value = directeurModel.telephone;
                        command.Parameters.Add("@Id_directeur", SqlDbType.Int).Value = Id_directeur; // Ajout de l'ID


                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            directeurModel.Id_directeur = Id_directeur;
            return directeurModel;
        }
    }
}
