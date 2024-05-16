using System.Data;
using System.Data.SqlClient;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Repository
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=contratTest;Trusted_Connection=True;";

        public async Task<Expert> CreateExpertAsync(Expert expertModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("AddExpert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@name_expert", SqlDbType.VarChar, 50).Value = expertModel.name_expert;
                    command.Parameters.Add("@lastname", SqlDbType.VarChar, 50).Value = expertModel.lastname;
                    command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = expertModel.email;
                    command.Parameters.Add("@telephone", SqlDbType.VarChar, 50).Value = expertModel.telephone;
                    command.Parameters.Add("@company", SqlDbType.VarChar, 50).Value = expertModel.company;
                    command.Parameters.Add("@Id_expertise", SqlDbType.Int).Value = expertModel.Id_expertise;

                    await command.ExecuteNonQueryAsync();

                }

                return expertModel;
            }
        }     

        public async Task<bool> DeleteExpertAsync(int Id_expert)
        {
            var expertDelete = await GetExpertByIdAsync(Id_expert);

            if (expertDelete != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteExpert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Id_expert", Id_expert);

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

        /*  public async Task<List<Expert>> GetAllExpertAsync()
          {
              var experts = new List<Expert>();

              using (var connection = new SqlConnection(_connectionString))
              {
                  await connection.OpenAsync();

                  using (var command = new SqlCommand("GetAllExpert", connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      using (var reader = await command.ExecuteReaderAsync())
                      {
                          while (await reader.ReadAsync())
                          {
                              var expert = new Expert();
                              expert.Id_expert = reader.GetInt32(0);
                              expert.name_expert = reader.GetString(1);
                              expert.lastname = reader.GetString(2);
                              expert.email = reader.GetString(3);
                              expert.telephone = reader.GetString(4);
                              expert.company = reader.GetString(5);
                              expert.Id_expertise = reader.GetInt32(6);

                              experts.Add(expert);
                          }
                      }
                  }
              }
              return experts;
          }
  */

        public async Task <List<Expert>> GetAllExpertAsync()
        {
            List<Expert> experts = new List<Expert>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("GetAllExpert", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Expert expert = new Expert();
                    expert.Id_expert = (int)reader["Id_expert"];
                    expert.name_expert = reader["name_expert"].ToString();
                    expert.lastname = reader["lastname"].ToString();
                    expert.email = reader["email"].ToString();
                    expert.telephone = reader["telephone"].ToString();
                    expert.company = reader["company"].ToString();
                    expert.Id_expertise = (int)reader["Id_expertise"];
                    expert.expertise = reader["expertise"].ToString(); // Nom de l'expertise récupéré de la procédure stockée

                    experts.Add(expert);
                }

                reader.Close();
            }

            return experts;
        }
/*
        public async Task<List<Expertise>> GetAllExpertisesAsync()
        {
            var expertises = new List<Expertise>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetAllExpertise", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var expertise = new Expertise();
                            expertise.Id_expertise = reader.GetInt32(0);
                            expertise.name_expertise = reader.GetString(1);

                            expertises.Add(expertise);
                        }
                    }
                }
            }
            return expertises;
        }*/

        public async Task<Expert> GetExpertByIdAsync(int Id_expert)
        {
            var expert = new Expert();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetIdExpert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id_expert", Id_expert);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            expert = new Expert
                            {
                                Id_expert = reader.GetInt32(0),
                                name_expert = reader.GetString(1),
                                lastname = reader.GetString(2),
                                email = reader.GetString(3),
                                telephone = reader.GetString(4),
                                company = reader.GetString(5),
                                Id_expertise = reader.GetInt32(6),
                            };

                        }
                    }
                }
                return expert;
            }
        }

        public async Task<Expert> UpdateExpertAsync(int Id_expert, Expert expertModel)
        {
            var updateExpert = await GetExpertByIdAsync(Id_expert);
            if (updateExpert != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("UpdateExpertise", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@name_expert", SqlDbType.VarChar, 50).Value = expertModel.name_expert;
                        command.Parameters.Add("@lastname", SqlDbType.VarChar, 50).Value = expertModel.lastname;
                        command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = expertModel.email;
                        command.Parameters.Add("@telephone", SqlDbType.VarChar, 50).Value = expertModel.telephone;
                        command.Parameters.Add("@company", SqlDbType.VarChar, 50).Value = expertModel.company;
                        command.Parameters.Add("@expertiseId", SqlDbType.Int).Value = expertModel.Id_expertise;

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            expertModel.Id_expert = Id_expert;
            return expertModel;
        }
    }
}
