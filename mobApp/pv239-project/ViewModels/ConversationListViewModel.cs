using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Models;
using pv239_project.Services;
using System.Collections.ObjectModel;

namespace pv239_project.ViewModels;

public partial class ConversationListViewModel(IConversationClient conversationClient) : ObservableObject
{
    [ObservableProperty]
    public partial ObservableCollection<ConversationList>? Items { get; set; }


    public async Task LoadConversationsAsync()
    {
        IEnumerable<ConversationPreviewDto> dtos = await conversationClient.Conversation_GetConversationsPreviewsByMemberIdAsync(1);
        Items = dtos.Select(s => new ConversationList
        {
            ConversationId = s.ConversationId,
            Title = s.Name,
            LastMessage = s.LastMessage
            
        }).ToList().ToObservableCollection();
    }

    
    [RelayCommand]
    private async Task GoToDetailAsync(int id)
    {
        await Shell.Current.GoToAsync(RoutingService.ConversationDetailPage,
           new Dictionary<string, object>
           {
               [nameof(ConversationDetailViewModel.ConversationId)] = id,
           });
    }
}
