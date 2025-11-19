using CommunityToolkit.Mvvm.Messaging;

namespace Receptkonyv_MAUI;

public partial class EditRecipePage : ContentPage
{
	public EditRecipePage(EditRecipePageViewModel VM)
	{
		InitializeComponent();
		BindingContext = VM;
        WeakReferenceMessenger.Default.Register<string>(this, (r, msg) =>
        {
            DisplayAlert("Warning", msg, "OK");

        });
    }
}