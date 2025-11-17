namespace Receptkonyv_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("recipePage", typeof(RecipePage));
            Routing.RegisterRoute("filterPage", typeof(FilterPage));
        }
    }
}
