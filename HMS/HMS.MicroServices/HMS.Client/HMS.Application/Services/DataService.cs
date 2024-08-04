using HMS.Application.Mapper;
using HMS.Client.Models.Input;
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
        internal DataService(ClientMapper mapper) 
        {
            Mapper = mapper;
        }
    }
}
