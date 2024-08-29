using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Employee.Core.Entity.Base
{
    public class BaseEntityWithEmployeeId : BaseEntity
    {
        public Guid EmployeeId { get; set; }
    }
}
