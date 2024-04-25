using System.Data;
using System.Data.SqlClient;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Repository
{
    public class FamillyRepository : IFamillyRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=contratTest;Trusted_Connection=True;";

        //private readonly string _connectionString = "Data Source=Valdou-MG;Initial Catalog=contratTest;Integrated Security=True;Pooling=False;Encrypt=False;TrustServerCertificate=False;";
        public FamillyRepository()
        {
          
        }
        /* public async Task<Familly> CreateAsync(Familly famillyModels)
         {
             using (var connectionString = new SqlConnection(_connectionString))
             {
                 await connectionString.OpenAsync();

                 using (var command = new SqlCommand("AddFamilly", connectionString))
                 {
                     command.CommandType = CommandType.StoredProcedure;
                     command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = famillyModels.name;

                     await command.ExecuteNonQueryAsync();
                 }
             }

             return famillyModels;
         }*/


        public async Task<Familly> CreateAsync(Familly famillyModels)
        {
            using (var connectionString = new SqlConnection(_connectionString))
            {
                await connectionString.OpenAsync();

                // Vérifier si le nom existe déjà
                using (var checkCommand = new SqlCommand("SELECT COUNT(*) FROM Familly WHERE Name = @Name", connectionString))
                {
                    checkCommand.Parameters.AddWithValue("@Name", famillyModels.name);
                    int existingCount = (int)await checkCommand.ExecuteScalarAsync();

                    // Si le nom existe déjà, retourner null ou lancer une exception, selon votre logique d'application
                    if (existingCount > 0)
                    {
                        // Nom déjà existant, vous pouvez lancer une exception ou retourner null, selon votre logique
                        throw new InvalidOperationException("Le nom de la famille existe déjà.");
                        // Ou
                        // return null;
                    }
                }

                // Création de la famille si le nom n'existe pas encore
                using (var command = new SqlCommand("AddFamilly", connectionString))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = famillyModels.name;

                    await command.ExecuteNonQueryAsync();
                }
            }

            return famillyModels;
        }




        public async Task<Familly> DeleteAsync(int id)
        {

            var deleteFamilly = await GetByIdAsync(id); 

           if(deleteFamilly != null) {  
               using (var connection = new SqlConnection(_connectionString))
               {
                    connection.OpenAsync();

                    using(var command = new SqlCommand("DeleteFamilly", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Id", id);

                        command.ExecuteNonQueryAsync();
                    }
               }
           }

            return deleteFamilly;

        }



        public async Task<List<Familly>> GetAllAsync()
        {
            var famillies = new List<Familly>();

            using ( var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(var command = new SqlCommand("GetAllFamilly", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using(var reader = await command.ExecuteReaderAsync()) 
                    {
                        while (await reader.ReadAsync())
                        {
                            var familly = new Familly();

                            familly.Id = reader.GetInt32(0);
                            familly.name = reader.GetString(1);

                            famillies.Add(familly);
                        }
                    }
                   
                }
            }
            return famillies;
        }

        public async Task<List<Familly>> GetByNameAsync(string name)
        {
            var famillies = new List<Familly>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetFamillyName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var familly = new Familly();

                            familly.Id = reader.GetInt32(0);
                            familly.name = reader.GetString(1);

                            famillies.Add(familly);
                        }
                    }

                }
            }
            return famillies;
        }

        public async Task<Familly> GetByIdAsync(int id)
        {
            Familly familly = new Familly();

            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(var command = new SqlCommand("GetByIdFamilly", connection))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            familly = new Familly
                            {
                                Id = reader.GetInt32(0), // Utilisez GetOrdinal pour obtenir l'index de la colonne
                                name = reader.GetString(1),
                            };
                        }
                    }
                }

                return familly;
            }
        }

        public async Task<Familly> UpdateAsync(int id, Familly famillyModels)
        {
            var existFamilly = GetByIdAsync(id);

            if(existFamilly == null)
            {
                return null;
            }
            else
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("UpdateFamilly", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Id", id);
                        command.Parameters.AddWithValue("Name", famillyModels.name);
                       

                        await command.ExecuteNonQueryAsync();
                    }
                }

                famillyModels.Id = id;
                return famillyModels;
            }
            
        }
    }
}
