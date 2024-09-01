using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Employee.Core.Interface.Manager
{
    public interface IManagerWithEmployeeId<Response, Input> : IManager<Response, Input>
    {
        public Task<Response> GetByEmployeeId(Guid ID);
    }
}
