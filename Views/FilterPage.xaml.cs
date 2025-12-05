namespace Receptkonyv_MAUI;

public partial class FilterPage : ContentPage
{
	public FilterPage(FilterPageViewModel VM)
	{
        InitializeComponent();
        BindingContext = VM;

	}
}