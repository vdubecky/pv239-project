using pv239_project.Client;
using pv239_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pv239_project.Mappers
{
    public static class DtoMapper
    {
        public static CreateMessageDto MessageToDto(this Message message)
        {
            return new()
            {
                SenderId = message.SenderId,
                Content = message.Content
            };
        }
    }
}
