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
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();        
        _viewModel.OnDisappearing();
    }
}

