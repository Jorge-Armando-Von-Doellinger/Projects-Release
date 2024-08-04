using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Client.Models.Input
{
    public class UpdateModel : InputModel
    {
        public long ID { get; set; }
    }

    public class UpdateModel<T, ID> : InputModel<T> where T : class
    {
        public UpdateModel(T value) : base(value)
        {}
        public ID Id { get; set; } 
    }
}
