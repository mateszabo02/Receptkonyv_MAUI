namespace Receptkonyv_MAUI
{
    public partial class RecipeDetailPage : ContentPage
    {
        //private RecipeDetailPageViewModel viewModel;
        public RecipeDetailPage(RecipeDetailPageViewModel VM)
        {
            InitializeComponent();
            VM = new RecipeDetailPageViewModel();
            BindingContext = VM;
        }
    }
}