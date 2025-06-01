using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class SettingsPage : ContentPage
{
    private UserSettingsViewModel _viewModel;

    public SettingsPage(UserSettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
    }
}

