using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class ConversationDetailPage : ContentPage
{
    protected ConversationDetailViewModel ViewModel { get; }

    public ConversationDetailPage(ConversationDetailViewModel conversationDetailViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = conversationDetailViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.OnStart();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();        
        await ViewModel.OnDestroy();
    }
}

