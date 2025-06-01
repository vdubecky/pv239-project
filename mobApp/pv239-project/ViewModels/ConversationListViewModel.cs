using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Models;
using pv239_project.Services;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

public partial class ConversationListViewModel(IConversationsService conversationService) : ObservableObject
{
    [ObservableProperty]
    public partial ObservableCollection<ConversationPreview>? Items { get; set; } = conversationService.Conversations;
    
    
    [RelayCommand]
    private async Task GoToDetailAsync(ConversationPreview preview)
    {
        conversationService.SelectedConversation = preview;
        await Shell.Current.GoToAsync(RoutingService.ConversationDetailPage);
    }
}
