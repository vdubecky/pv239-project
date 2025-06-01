using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public class Member : ObservableObject
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
}