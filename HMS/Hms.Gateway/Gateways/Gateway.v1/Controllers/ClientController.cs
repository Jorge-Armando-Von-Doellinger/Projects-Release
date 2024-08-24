using Gateway.v1.Application.Enums;
using Gateway.v1.Application.Interfaces;
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
        private readonly IManager _clientManager;
        public ClientController(Func<Enum, IManager> factory)
        {
            _clientManager = factory(ManageFactoryEnums.ClientManager);
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _clientManager.Get());
        }
        [HttpGet("ID")]
        public async Task<IActionResult> GetClientByID(long ID)
        {
            try 
            {
                return Ok(await _clientManager.GetById(ID));
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
                return Ok(await _clientManager.Add(input));
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(UpdateModel input)
        {
            try
            {
                return Ok(await _clientManager.Update(input));
            }
            catch (Exception ex)
            {
                throw;  
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(long ID)
        {
            try
            {
                return Accepted(await _clientManager.Delete(ID));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
