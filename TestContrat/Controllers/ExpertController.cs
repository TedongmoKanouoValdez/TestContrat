using Microsoft.AspNetCore.Mvc;
using TestContrat.Dtos;
using TestContrat.Models;
using TestContrat.Referentiels;
using TestContrat.Repository;

namespace TestContrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertController : ControllerBase
    {
        private readonly IExpertRepository _expertRepository;

        public ExpertController (IExpertRepository expertRepository)
        {
            _expertRepository = expertRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpert()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var getAllExpert = await _expertRepository.GetAllExpertAsync();
                return Ok(getAllExpert);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpert([FromBody] ExpertDto expertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var expert = new Expert
                {
                    name_expert = expertDto.name_expert,
                    lastname = expertDto.lastname,
                    email = expertDto.email,
                    telephone = expertDto.telephone,
                    company = expertDto.company,
                    Id_expertise = expertDto.Id_expertise,
                };

                var createdExpert = await _expertRepository.CreateExpertAsync(expert);
                return Ok(createdExpert);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpertById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultById = await _expertRepository.GetExpertByIdAsync(id);
                return Ok(resultById);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpert(int id, ExpertDto expertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var expert = new Expert
                {
                    name_expert = expertDto.name_expert,
                    lastname = expertDto.lastname,
                    email = expertDto.email,
                    telephone = expertDto.telephone,
                    company = expertDto.company,
                    Id_expertise = expertDto.Id_expertise,
                };
                var updateResult = await _expertRepository.UpdateExpertAsync(id, expert);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpert(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deleteResult = await _expertRepository.DeleteExpertAsync(id);
                return Ok(deleteResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
