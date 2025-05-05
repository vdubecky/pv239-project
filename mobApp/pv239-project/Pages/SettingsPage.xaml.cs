using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class SettingsPage : ContentPage
{
    protected UserSettingsViewModel ViewModel { get; }

    public SettingsPage(UserSettingsViewModel viewModel)
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

