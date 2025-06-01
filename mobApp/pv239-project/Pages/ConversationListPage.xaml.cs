using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class ConversationListPage : ContentPage
{
    private readonly ConversationListViewModel _viewModel;

    public ConversationListPage(ConversationListViewModel conversationListViewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = conversationListViewModel;
    }
}