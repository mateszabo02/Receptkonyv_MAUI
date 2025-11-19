using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.CompilerServices;

namespace Receptkonyv_MAUI
{
    public partial class MainPage : ContentPage
    {
        //private MainPageViewModel viewModel;
        public MainPage(MainPageViewModel VM)
        {
            InitializeComponent();
            VM = new MainPageViewModel();
            BindingContext = VM;
        }
    }
}
