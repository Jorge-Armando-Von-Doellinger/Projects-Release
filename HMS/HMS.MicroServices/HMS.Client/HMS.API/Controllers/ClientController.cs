using HMS.Application.Managers;
using HMS.Client.Models.Input;
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
        public async Task<IActionResult> GetClientsAsync(InputModel input)
        {
            Console.WriteLine(input.Name);
            await Task.CompletedTask;
            return Ok();
        }
    }
}
