using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pv239_project.ViewModels;

namespace pv239_project.Pages;

public partial class ConversationDetailPage : ContentPage
{
    public ConversationDetailPage()
    {
        InitializeComponent();
        BindingContext = new ConversationDetailViewModel();
    }
}

