using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Models;
using pv239_project.Services;

namespace pv239_project.ViewModels;

public partial class ConversationListViewModel : ObservableObject
{
    [ObservableProperty]
    public partial ObservableCollection<ConversationList>? Items { get; set; }

    public ConversationListViewModel()
    {
        Items = new ObservableCollection<ConversationList>
        {
            new() { Id = Guid.NewGuid(), Title = "Bob", LastMessage = "Hey, how's it going?", LastActivity = DateTime.Now.AddMinutes(-10) },
            new() { Id = Guid.NewGuid(), Title = "David", LastMessage = "See you tomorrow!", LastActivity = DateTime.Now.AddHours(-1) },
            new() { Id = Guid.NewGuid(), Title = "Frank", LastMessage = "Thanks for the update.", LastActivity = DateTime.Now.AddMinutes(-30) },
            new() { Id = Guid.NewGuid(), Title = "Heidi", LastMessage = "I'll send you the details.", LastActivity = DateTime.Now.AddDays(-1) },
            new() { Id = Guid.NewGuid(), Title = "Judy", LastMessage = "Sounds good!", LastActivity = DateTime.Now.AddMinutes(-5) }
        };
    }
    
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await Shell.Current.GoToAsync(RoutingService.ConversationDetailPage,
            new Dictionary<string, object>
            {
                [nameof(ConversationDetailViewModel.Id)] = id
            });
    }
}
