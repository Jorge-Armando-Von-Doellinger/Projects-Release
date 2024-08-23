using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.v1.Core.Messaging.Listener
{
    public interface IMessageListener
    {
        public Task<object> GetMessage(string baseResponse, Guid messageID);
    }
}
