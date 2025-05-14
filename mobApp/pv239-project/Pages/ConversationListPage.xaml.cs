using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class ConversationListPage : ContentPage
{
    protected ConversationListViewModel ViewModel { get; }

    public ConversationListPage(ConversationListViewModel conversationListViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = conversationListViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadConversationsAsync();
    }
}

