using Microsoft.AspNetCore.Mvc;
using TestContrat.Dtos;
using TestContrat.Models;
using TestContrat.Referentiels;
using TestContrat.Repository;

namespace TestContrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController: ControllerBase
    {
        private IClientRepository _clientRepositoy;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepositoy = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClient()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var getAllClient = await _clientRepositoy.GetAllClientAsync();
                return Ok(getAllClient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = new Client
                {
                    Business_area = clientDto.Business_area,
                    Company_name = clientDto.Company_name,
                    Parent_company_name = clientDto.Parent_company_name,
                };

                var createdClient = await _clientRepositoy.CreateClientAsync(client);
                return Ok(createdClient);
            }
            catch(Exception ex) 
            {
                throw ex;        
                    
            }
        }
    }
}
