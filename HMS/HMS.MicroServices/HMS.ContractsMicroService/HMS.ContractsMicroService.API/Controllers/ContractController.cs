using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Contracts.Inputs;
using System.Linq.Expressions;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IEmployeeContractManager _manager;

        public ContractController(IEmployeeContractManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetContracts()
            => Ok(await _manager.GetAll());

        [HttpGet("ID")]
        public async Task<IActionResult> GetContractById(Guid ID)
        {
            try
            {
                return Ok(await _manager.GetById(ID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContract(EmployeeContractInput contract)
        {
            try
            {
                if(contract.HaveAPropertyDefault())
                    Console.WriteLine(" \n Tem valores default \n");
                await _manager.Add(contract);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContract(EmployeeContractInput input)
        {
            try
            {
                await _manager.Update(input);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
