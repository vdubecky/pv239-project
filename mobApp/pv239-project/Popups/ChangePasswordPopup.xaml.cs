using CommunityToolkit.Maui.Views;
using pv239_project.ViewModels;

namespace pv239_project.Popups;

public partial class ChangePasswordPopup : Popup
{
    public ChangePasswordPopup(ChangePasswordPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

