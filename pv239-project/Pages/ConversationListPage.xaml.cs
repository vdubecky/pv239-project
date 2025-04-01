using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class ConversationListPage : ContentPage
{
    public ConversationListPage()
    {
        InitializeComponent();
        BindingContext = new ConversationListViewModel();
    }
}

