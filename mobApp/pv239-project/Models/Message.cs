using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public class Message : ObservableObject
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public string Content { get; set; } 
}
