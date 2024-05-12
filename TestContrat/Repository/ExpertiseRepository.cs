using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Repository
{
    public class ExpertiseRepository : IExpertiseRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=contratTest;Trusted_Connection=True;";

        public async Task<Expertise> CreateExpertiseAsync(Expertise expertiseModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("AddExpertise", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@name_expertise", SqlDbType.VarChar, 50).Value = expertiseModel.name_expertise;

                    await command.ExecuteNonQueryAsync();
                }

                return expertiseModel;
            }
        }

        public async Task<bool> DeleteExpertiseAsync(int Id_expertise)
        {
            var expertiseDelete = await GetExpertiseByIdAsync(Id_expertise);

            if (expertiseDelete != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteExpertise", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Id_expertise", Id_expertise);

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

        public async Task<List<Expertise>> GetAllExpertiseAsync()
        {
            var expertises =new List<Expertise>();

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
        }

        public async Task<Expertise> GetExpertiseByIdAsync(int Id_expertise)
        {
            var expertise = new Expertise();

            using (var connection = new SqlConnection(_connectionString)) 
            {
               await connection.OpenAsync();

               using (var command = new SqlCommand("GetIdExpertise", connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id_expertise", Id_expertise);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            expertise = new Expertise
                            {
                                Id_expertise = reader.GetInt32(0), // Utilisez GetOrdinal pour obtenir l'index de la colonne
                                name_expertise = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return expertise;
        }


        public async Task<Expertise> UpdateExpertiseAsync(int Id_expertise, Expertise expertiseModel)
        {
            var updateExpertise = await GetExpertiseByIdAsync(Id_expertise);
            if (updateExpertise != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("UpdateExpertise", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Name_expertise",expertiseModel.name_expertise);
                        command.Parameters.AddWithValue("Id_expertise", Id_expertise);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            expertiseModel.Id_expertise = Id_expertise;
            return expertiseModel;
        }
    }
}