using HMS.Application.Mapper;
using Nuget.Clients.DTOs.Input;
using HMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Application.Services
{
    public sealed class DataService
    {
        internal ClientMapper Mapper { get; set; }
        public DataService(ClientMapper mapper) 
        {
            Mapper = mapper;
        }

         
    }
}
