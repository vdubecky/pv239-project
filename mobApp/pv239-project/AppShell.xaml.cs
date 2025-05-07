using pv239_project.ViewModels;

namespace pv239_project;

public partial class AppShell : Shell
{
    public AppShell(AuthViewModel authViewModel)
    {
        InitializeComponent();
        BindingContext = authViewModel;
    }
}
