using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class ConversationDetailPage : ContentPage
{
    private readonly ConversationDetailViewModel _viewModel;

    public ConversationDetailPage(ConversationDetailViewModel conversationDetailViewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = conversationDetailViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearing();
        
        if (_viewModel.Conversation != null)
        {
            MessagesCollectionView.ScrollTo(_viewModel.Conversation.Messages.Count - 1, 
                position: ScrollToPosition.End, animate: false);  
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();        
        _viewModel.OnDisappearing();
    }
}

