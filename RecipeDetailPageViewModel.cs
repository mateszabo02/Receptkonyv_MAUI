using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Receptkonyv_MAUI
{
    [QueryProperty(nameof(ViewedRecipe), "Recipe")]
    public partial class RecipeDetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipe viewedRecipe;
        [ObservableProperty]
        Recipe draft;
        public Recipe EditedRecipe
        {
            set
            {
                EditedRecipe = ViewedRecipe;
            }
        }
        partial void OnViewedRecipeChanged(Recipe value)
        {
            InitDraft(value);
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
    }
}
