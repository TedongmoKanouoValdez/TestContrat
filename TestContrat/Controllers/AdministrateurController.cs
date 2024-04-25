using Microsoft.AspNetCore.Mvc;
using TestContrat.Dtos;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrateurController : ControllerBase
    {
            private readonly IAdministrateurRepository _adminrepository;
            public AdministrateurController(IAdministrateurRepository adminRepository)
            {
               _adminrepository = adminRepository;
            }

            [HttpPost]
            public async Task<IActionResult> CreatedAdmin([FromBody] AdministrateurDto adminDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    //on cree une nouvelle instance de famille à partir des données
                    //de familleDto vu que la methode attends un objet du type famille
                    var admin = new Administrateur
                    {
                          firstname = adminDto.Firstname,
                          lastname = adminDto.Lastname,
                          email = adminDto.Email,
                          phoneNumber = adminDto.PhoneNumber,
                    
                    };

                    var createResult = await _adminrepository.CreateAdminAsync(admin);
                    return Ok(createResult);
                }
                catch (Exception ex)
                {
                    /*ModelState.AddModelError("Famille", "Une erreur s'est produite lors de l'insertion en base de données.");
                    return BadRequest(ModelState);*/

                    throw ex;
                }
            }

            [HttpGet("all")]
            public async Task<IActionResult> GetAllAdmin()
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var getResult = await _adminrepository.GetAllAdminsAsync();
                    return Ok(getResult);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("admin", "Une erreur s'est produite.");
                    return BadRequest(ModelState);
                }

            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetAdminById(int id)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var resultById = await _adminrepository.GetAdminByIdAsync(id);
                    return Ok(resultById);
                }
                catch (Exception ex)
                {
                throw ex;
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateAdmin(int id, AdministrateurDto administrateurDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                var admin = new Administrateur
                {
                    firstname = administrateurDto.Firstname,
                    lastname = administrateurDto.Lastname,
                    email = administrateurDto.Email,
                    phoneNumber = administrateurDto.PhoneNumber,
                };
                var updateResult = await _adminrepository.UpdateAdminAsync(id, admin);
                    return Ok(updateResult);
                }
                catch (Exception ex)
                {
                throw ex;
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAdmin(int id)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var deleteResult = await _adminrepository.DeleteAdminAsync(id);
                    return Ok(deleteResult);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
    }
}


