using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class UserListPage : ContentPage
{
    private readonly UserListViewModel _viewModel;

    public UserListPage(UserListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }
}
