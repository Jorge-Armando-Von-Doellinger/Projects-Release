using Gateway.v1.Application.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Client.Input;

namespace Gateway.v1.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientManager _clientManager;
        public ClientController(ClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _clientManager.GetClientsAsync());
        }
        [HttpGet("ID")]
        public async Task<IActionResult> GetClientByID(long ID)
        {
            try{
                return Ok(await _clientManager.GetClientByIdAsync(ID));

            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(InputModel input)
        {
            try
            {
                return Accepted(_clientManager.AddClientAsync(input));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
