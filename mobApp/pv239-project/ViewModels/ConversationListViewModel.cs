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
            new() { ConversationId = 1, Title = "Bob", LastMessage = "Hey, how's it going?"},
            new() { ConversationId = 2, Title = "David", LastMessage = "See you tomorrow!"},
            new() { ConversationId = 3, Title = "Frank", LastMessage = "Thanks for the update."},
            new() { ConversationId = 4, Title = "Heidi", LastMessage = "I'll send you the details."},
            new() { ConversationId = 5, Title = "Judy", LastMessage = "Sounds good!"}
        };
    }
    
    [RelayCommand]
    private async Task GoToDetailAsync(int id)
    {
        await Shell.Current.GoToAsync(RoutingService.ConversationDetailPage,
            new Dictionary<string, object>
            {
                [nameof(ConversationDetailViewModel.Id)] = id
            });
    }
}
