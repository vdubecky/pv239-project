using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public class ConversationDetail : ObservableObject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ObservableCollection<Guid> Participants { get; set; }
    public ObservableCollection<Message> Messages { get; set; }
}
