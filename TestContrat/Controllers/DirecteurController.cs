using Microsoft.AspNetCore.Mvc;
using TestContrat.Dtos;
using TestContrat.Models;
using TestContrat.Referentiels;

namespace TestContrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirecteurController: ControllerBase
    {
        private readonly IDirecteurRepository _directeurRepository;

        public DirecteurController(IDirecteurRepository directeurRepository)
        {
            _directeurRepository = directeurRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDirecteur()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var getAllDirecteur = await _directeurRepository.GetAllDirecteurAsync();
                return Ok(getAllDirecteur);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirecteur([FromBody] DirecteurDto directeurDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var directeur = new Directeur
                {
                    name_directeur = directeurDto.name_directeur,
                    lastname_directeur = directeurDto.lastname_directeur,
                    email = directeurDto.email,
                    telephone = directeurDto.telephone,

                };

                var createdDirecteur = await _directeurRepository.CreateDirecteurAsync(directeur);
                return Ok(createdDirecteur);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirecteurById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultById = await _directeurRepository.GetDirecteurByIdAsync(id);
                return Ok(resultById);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirecteur(int id, DirecteurDto directeurDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var directeur = new Directeur
                {
                    name_directeur = directeurDto.name_directeur,
                    lastname_directeur = directeurDto.lastname_directeur,
                    email=directeurDto.email,
                    telephone=directeurDto.telephone,
                };
                var updateResult = await _directeurRepository.UpdateDirecteurAsync(id, directeur);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirecteur(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deleteResult = await _directeurRepository.DeleteDirecteurAsync(id);
                return Ok(deleteResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

