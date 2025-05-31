using System.Collections.ObjectModel;
using pv239_project.Models;

namespace pv239_project.Services.Interfaces;

public interface IConversationsService
{
    public ObservableCollection<ConversationPreview> Conversations { get; }
    public ConversationPreview SelectedConversation { get; set; }

    void SortConversationsByLastMessage(ConversationPreview preview);
    Task Init();
}