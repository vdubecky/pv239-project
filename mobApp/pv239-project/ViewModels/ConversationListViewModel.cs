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
            new() { Title = "Bob", LastMessage = "Hey, how's it going?"},
            new() { Title = "David", LastMessage = "See you tomorrow!"},
            new() { Title = "Frank", LastMessage = "Thanks for the update."},
            new() { Title = "Heidi", LastMessage = "I'll send you the details."},
            new() { Title = "Judy", LastMessage = "Sounds good!"}
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
