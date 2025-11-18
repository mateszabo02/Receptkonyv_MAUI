using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.CompilerServices;

namespace Receptkonyv_MAUI
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }
    }
}
