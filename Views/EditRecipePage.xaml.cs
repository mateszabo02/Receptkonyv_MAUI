using CommunityToolkit.Mvvm.Messaging;

namespace Receptkonyv_MAUI;

public partial class EditRecipePage : ContentPage
{
	public EditRecipePage(EditRecipePageViewModel VM)
	{
		InitializeComponent();
		BindingContext = VM;

		WeakReferenceMessenger.Default.Register<string>(this, async(r, m) =>
		{
			DisplayAlert("Error", m, "OK");
		});
    }
}