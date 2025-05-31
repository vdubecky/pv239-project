using pv239_project.Models;

namespace pv239_project.Resources.Selectors;

public class MessageSelector : DataTemplateSelector
{
    public static readonly DataTemplate EmptyTemplate = new(() => new ContentView());
    
    public required DataTemplate InMessage { get; set; }
    public required DataTemplate OutMessage { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is not Message message)
        {
            return EmptyTemplate;    
        }

        return message.IsOutgoing ? OutMessage : InMessage;
    }
}