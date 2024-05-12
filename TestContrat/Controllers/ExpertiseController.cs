using Microsoft.AspNetCore.Mvc;
using TestContrat.Dtos;
using TestContrat.Models;
using TestContrat.Referentiels;
using TestContrat.Repository;

namespace TestContrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertiseController: ControllerBase
    {
        private readonly IExpertiseRepository _expertiseRepository;

        public ExpertiseController(IExpertiseRepository expertiseRepository)
        {
            _expertiseRepository = expertiseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpertise()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var getAllExpertise = await _expertiseRepository.GetAllExpertiseAsync();
                return Ok(getAllExpertise);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpertise([FromBody] ExpertiseDto expertiseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var expertise = new Expertise
                {
                    name_expertise = expertiseDto.name_expertise
                };

                var createdExpertise = await _expertiseRepository.CreateExpertiseAsync(expertise);
                return Ok(createdExpertise);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpertiseById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultById = await _expertiseRepository.GetExpertiseByIdAsync(id);
                return Ok(resultById);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpertise(int id, ExpertiseDto expertiseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var expertise = new Expertise
                {
                    name_expertise = expertiseDto.name_expertise
                };
                var updateResult = await _expertiseRepository.UpdateExpertiseAsync(id, expertise);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpertise(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deleteResult = await _expertiseRepository.DeleteExpertiseAsync(id);
                return Ok(deleteResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
