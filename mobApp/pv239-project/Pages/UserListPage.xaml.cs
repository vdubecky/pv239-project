using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class UserListPage : ContentPage
{
    protected UserListViewModel ViewModel { get; }

    public UserListPage(UserListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await ViewModel.LoadDataAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
