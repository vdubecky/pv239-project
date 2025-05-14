using pv239_project.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pv239_project.Services.Interfaces
{
    public interface IHubService
    {
         Dictionary<string, Action<CreateMessageDto>> MessageHandlers { get; set; }
         Task Start();
    }
}
