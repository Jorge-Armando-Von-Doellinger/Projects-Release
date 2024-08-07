using HMS.Application.Managers;
using Nuget.Clients.DTOs.Input;
using Nuget.Clients.DTOs.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
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


        [HttpPost]
        public async Task<IActionResult> AddClientAsync(InputModel input)
            => Ok(await _clientManager.AddClientAsync(input));

        [HttpPut]
        public async Task<IActionResult> UpdateClientAsync(UpdateModel input)
            => Ok(await _clientManager.UpdateClientAsync(input));

        [HttpDelete]
        public async Task<IActionResult> DeleteClientAsync(long ID)
            => Ok(await _clientManager.DeleteClientAsync(ID));
    }
}
