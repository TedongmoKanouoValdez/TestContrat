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
                    code_client = clientDto.Code_client,
                    business_area = clientDto.Business_area,
                    company_name = clientDto.Company_name,
                    parent_company_name = clientDto.Parent_company_name,
                };

                var createdClient = await _clientRepositoy.CreateClientAsync(client);
                return Ok(createdClient);
            }
            catch(Exception ex) 
            {
                throw ex;        
                    
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id_client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultById = await _clientRepositoy.GetClientByIdAsync(id_client);
                return Ok(resultById);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, ClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = new Client
                {
                    code_client = clientDto.Code_client,
                    business_area = clientDto.Business_area,
                    company_name = clientDto.Company_name,
                    parent_company_name = clientDto.Parent_company_name,
                };
                var updateResult = await _clientRepositoy.UpdateClientAsync(id, client);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deleteResult = await _clientRepositoy.DeleteClientASync(id);
                return Ok(deleteResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
