using HMS.Application.Managers;
using Nuget.Client.Input;
using Nuget.Client.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Response;

namespace HMS.API.Controllers
{
    [Route("client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientManager _clientManager;
        public ClientController(ClientManager clientManager) 
        {
            _clientManager = clientManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetClientsAsync()
            => Ok(await _clientManager.GetClientsAsync());
        [HttpGet("ID")]
        public async Task<IActionResult> GetClientByID(long ID)
            => Ok(await _clientManager.GetClientByIdAsync(ID));
    }
}
