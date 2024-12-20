﻿using HMS.ContractsMicroService.API.Settings;
using HMS.ContractsMicroService.API.Settings.Interfaces;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema;
using System.Text.Json;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IHostEnvironment _environment;
        private readonly IEnumerable<OnUpdatedSettings> _onUpdatedSettings;
        private readonly IAppSettings _appSettings;
        private const string SettingsSchemaKey = Keys.AppSettingsJson;
        public SettingsController(ISettingsService settingsService, 
            IHostEnvironment environment, 
            IEnumerable<OnUpdatedSettings> onUpdatedSettings, 
            IAppSettings appSettings)
        {
            _settingsService = settingsService;
            _environment = environment;
            _onUpdatedSettings = onUpdatedSettings;
            _appSettings = appSettings;
        }

        /// <summary>
        ///     Recebe dados de configuração, de acordo com o schema
        /// </summary>
        /// <param name="settings"></param>
        /// <returns> status 200 </returns>
        [HttpPost]
        public async Task<IActionResult> AddSettings(object settings)
        {
            var stringschema = await _settingsService.GetSchema(Keys.AppSettingsJson);
            var schema = await JsonSchema.FromJsonAsync(stringschema);
            var settingsRecieved = JsonSerializer.Serialize(settings);
            /*Console.WriteLine(stringschema + " Esse é o schema");
            Console.WriteLine(schema.ToJson());*/
            var errors = schema.Validate(settingsRecieved);
            
            foreach (var error in errors) 
            {
                Console.WriteLine(error.Kind);
                Console.WriteLine(error.Path);
                Console.WriteLine(error.Property);
            }

            if (errors.Count == 0)
            {
                foreach(var e in _onUpdatedSettings)
                {
                    await _settingsService.UpdateSettings(e, settingsRecieved, Keys.AppSettingsJson);
                }
                return Accepted();
            }

            return BadRequest("Schema não válido!");
        }
    }
}
