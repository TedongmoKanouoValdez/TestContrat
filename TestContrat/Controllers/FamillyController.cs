using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamillyController : ControllerBase
    {
        private readonly IFamillyRepository _famillyRepository;
        public FamillyController(IFamillyRepository famillyRepository)
        {
            _famillyRepository = famillyRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatedFamilly([FromBody] FamillyDto familleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //on cree une nouvelle instance de famille à partir des données
                //de familleDto vu que la methode attends un objet du type famille
                var famille = new Familly
                {
                    name = familleDto.Name

                };

                var createResult = await _famillyRepository.CreateAsync(famille);
                return Ok(createResult);
            }
            catch (Exception ex)
            {
                /*ModelState.AddModelError("Famille", "Une erreur s'est produite lors de l'insertion en base de données.");
                return BadRequest(ModelState);*/

                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFamilly()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var getResult = await _famillyRepository.GetAllAsync();
                return Ok(getResult);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Famille", "Une erreur s'est produite.");
                return BadRequest(ModelState);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFamilly(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultById = await _famillyRepository.GetByIdAsync(id);
                return Ok(resultById);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Famille", "Une erreur s'est produite.");
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFamilly(int id, FamillyDto familleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var familly = new Familly
                {
                    name = familleDto.Name,
                };
                var updateResult = await _famillyRepository.UpdateAsync(id, familly);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Famille", "Une erreur s'est produite.");
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilly(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deleteResult = await _famillyRepository.DeleteAsync(id);
                return Ok(deleteResult);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Famille", "Une erreur s'est produite.");
                return BadRequest(ModelState);
            }

        }
    }
}
