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
    [QueryProperty(nameof(EditedRecipe), "editedRecipe")]
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
                //ViewedRecipe.Ingredients = new System.Collections.ObjectModel.ObservableCollection<string>(value.Ingredients);
                InitDraft(ViewedRecipe);
            }
        }
        public void InitDraft(Recipe value)
        {
            if (value != null)
                Draft = value.GetCopy();
            else
            {
                Draft = new Recipe { Name = "---", Ingredients = new() { "-", "-" }, Description = "---" };
            }
        }
        [RelayCommand]
        public async Task EditRecipe()
        {
            var param = new ShellNavigationQueryParameters
            {
                { "Recipe", ViewedRecipe }
            };
            await Shell.Current.GoToAsync("editRecipePage", param);

        }
        [RelayCommand]
        public async Task BackToMainPage()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task DeleteRecipe()
        {
            WeakReferenceMessenger.Default.Send(new DeleteRecipeMessage(ViewedRecipe));
            await Shell.Current.GoToAsync("..");
        }
    }
    public class DeleteRecipeMessage : ValueChangedMessage<Recipe>
    {
        public DeleteRecipeMessage(Recipe recipe) : base(recipe)
        {
        }
    }
}
