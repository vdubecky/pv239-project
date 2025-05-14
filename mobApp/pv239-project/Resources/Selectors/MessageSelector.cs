using pv239_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pv239_project.Resources.Selectors
{
    public class MessageSelector : DataTemplateSelector
    {
        public static readonly DataTemplate EmptyTemplate = new DataTemplate(() => new ContentView());

        public required DataTemplate InMessage { get; set; }
        public required DataTemplate OutMessage { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if(item == null || item is not Message)
            {
                return EmptyTemplate;
            }

            Message message = item as Message;
            return message.SenderId == 1 ? OutMessage : InMessage;
        }
    }
}
