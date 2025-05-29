using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using pv239_project.Client;

namespace pv239_project.Models;

public class ConversationDetail : ObservableObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ObservableCollection<Member> Members { get; set; }
    public ObservableCollection<Message> Messages { get; set; }
}
