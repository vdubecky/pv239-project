using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using pv239_project.ViewModels;

namespace pv239_project.Popups;

public partial class UploadProfilePicturePopup : Popup
{
    public UploadProfilePicturePopup(UploadProfilePicturePopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

