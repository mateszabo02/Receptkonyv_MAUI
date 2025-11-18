namespace Receptkonyv_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("recipeDetailPage", typeof(RecipeDetailPage));
            Routing.RegisterRoute("filterPage", typeof(FilterPage));
        }
    }
}
