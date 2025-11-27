using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Receptkonyv_MAUI
{
    [QueryProperty(nameof(ViewedRecipe), "Recipe")]
    [QueryProperty(nameof(EditedRecipe), "EditedRecipe")]
    public partial class RecipeDetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipe viewedRecipe;
        [ObservableProperty]
        Recipe draft;
        [ObservableProperty]
        Recipe editedRecipe;
        partial void OnViewedRecipeChanged(Recipe value)
        {

            InitDraft(value);
        }
        partial void OnEditedRecipeChanged(Recipe value)
        {
            if (value != null && ViewedRecipe!=null)
            {
                ViewedRecipe.Name = value.Name;
                ViewedRecipe.Description = value.Description;
                ViewedRecipe.Ingredients = value.Ingredients;
                ViewedRecipe.ImageUrl = value.ImageUrl;
                ViewedRecipe.UpdateStrings();
                InitDraft(ViewedRecipe);
            }
        }
        public void InitDraft(Recipe value)
        {
            if (value != null)
            {
                Draft = value.GetCopy();
                Draft.UpdateStrings();
            }
            else
            {
                Draft = new Recipe();
            }
        }
        [RelayCommand]
        public async Task EditRecipeAsync()
        {
            var param = new ShellNavigationQueryParameters
            {
                { "Recipe", ViewedRecipe }
            };
            await Shell.Current.GoToAsync("editRecipePage", param);

        }
        [RelayCommand]
        public async Task BackToMainPageAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task DeleteRecipeAsync()
        {
            WeakReferenceMessenger.Default.Send(new DeleteRecipeMessage(ViewedRecipe));
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task ShareRecipeAsync()
        {
            if(ViewedRecipe == null)
                return;
            if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Title = "Share Recipe",
                    Text = ViewedRecipe.ToString()
                });
            }
            else
            {
                WeakReferenceMessenger.Default.Send("No internet access.");
            }
        }
    }
    public class DeleteRecipeMessage : ValueChangedMessage<Recipe>
    {
        public DeleteRecipeMessage(Recipe recipe) : base(recipe)
        {
        }
    }
}
