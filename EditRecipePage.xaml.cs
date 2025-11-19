namespace Receptkonyv_MAUI;

public partial class EditRecipePage : ContentPage
{
	//private EditRecipePageViewModel viewModel;
	public EditRecipePage(EditRecipePageViewModel VM)
	{
		InitializeComponent();
		VM = new EditRecipePageViewModel();
		BindingContext = VM;
	}
}