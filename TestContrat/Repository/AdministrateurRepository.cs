using TestContrat.Models;
using TestContrat.Referentiels;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace TestContrat.Repository
{
    public class AdministrateurRepository : IAdministrateurRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=contratTest;Trusted_Connection=True;";


        public async Task<Administrateur> CreateAdminAsync(Administrateur adminModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddAdministrateur", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = adminModel.firstname;
                    command.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = adminModel.lastname;
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = adminModel.email;
                    command.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = adminModel.phoneNumber;

                    await command.ExecuteNonQueryAsync();

                }
                return adminModel;
            }
        }





        /* public async Task<Administrateur> CreateAdminAsync(Administrateur adminModel)
         {

             using (var connection = new SqlConnection(_connectionString))
             {
                 await connection.OpenAsync();

                 // Récupérer tous les administrateurs existants
                 var existingAdmins = await GetAllAdminsAsync();

                 // Vérifier si un administrateur avec les mêmes données existe déjà
                 var existingAdmin = existingAdmins.FirstOrDefault(a =>
                     a.firstname == adminModel.firstname &&
                     a.lastname == adminModel.lastname &&
                     a.email == adminModel.email &&
                     a.phoneNumber == adminModel.phoneNumber
                 );

                 if (existingAdmin != null)
                 {
                     // Un administrateur avec les mêmes données existe déjà, donc retournez null ou lancez une exception
                     throw new Exception("Un administrateur avec les mêmes données existe déjà.");
                 }

                 // Si aucun administrateur avec les mêmes données n'existe, vous pouvez insérer les données dans la base de données
                 using (var command = new SqlCommand("AddAdministrateur", connection))
                 {
                     command.CommandType = CommandType.StoredProcedure;
                     command.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = adminModel.firstname;
                     command.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = adminModel.lastname;
                     command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = adminModel.email;
                     command.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = adminModel.phoneNumber;

                     await command.ExecuteNonQueryAsync();
                 }

                 return adminModel;
             }
         }*/



        
    

    public async Task<bool> DeleteAdminAsync(int IdAdmin)
        {
            var adminToDelete = await GetAdminByIdAsync(IdAdmin);

            if (adminToDelete != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteAdministrateur", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("IdAdmin", IdAdmin);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true; // Suppression réussie
            }
            else
            {
                return false; // L'administrateur n'existe pas
            }
        }

        public async Task<Administrateur> GetAdminByIdAsync(int id)
        {
            var administrateur = new Administrateur();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetByIdAdmin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdAdmin", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            administrateur = new Administrateur
                            {
                                IdAdmin = reader.GetInt32(0), // Utilisez GetOrdinal pour obtenir l'index de la colonne
                                firstname = reader.GetString(1),
                                lastname = reader.GetString(2),
                                email = reader.GetString(3),
                                phoneNumber = reader.GetString(4)
                            };
                        }
                    }
                }

                return administrateur;
            }
        }

        public async Task<List<Administrateur>> GetAllAdminsAsync()
        {
            var adminList = new List<Administrateur>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllAdministrateur", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var admin = new Administrateur();
                            admin.IdAdmin = reader.GetInt32(0);
                            admin.firstname = reader.GetString(1);
                            admin.lastname = reader.GetString(2);
                            admin.email = reader.GetString(3);
                            admin.phoneNumber = reader.GetString(4);

                            adminList.Add(admin);
                        }
                    }
                }
                return adminList;
            }
        }
        public async Task<Administrateur> UpdateAdminAsync(int IdAdmin, Administrateur adminModel)
        {
               var updateAdmin =await GetAdminByIdAsync(IdAdmin);
            if (updateAdmin != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("UpdateAdministrateur", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Firstname", adminModel.firstname);
                        command.Parameters.AddWithValue("Lastname", adminModel.lastname);
                        command.Parameters.AddWithValue("Email", adminModel.email);
                        command.Parameters.AddWithValue("PhoneNumber", adminModel.phoneNumber);
                        command.Parameters.AddWithValue("IdAdmin", IdAdmin);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            adminModel.IdAdmin = IdAdmin;
            return adminModel;

        }
      
    }
}
