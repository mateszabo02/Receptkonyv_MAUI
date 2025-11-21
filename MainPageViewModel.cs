using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        [ObservableProperty]
        private Recipe selectedRecipe;
        [ObservableProperty]
        string filterTerm;
        public Recipe ViewedRecipe
        {
            set
            {
                ViewedRecipe = SelectedRecipe;
            }
        }

        public MainPageViewModel()
        {
            Recipes = new ObservableCollection<Recipe>();
            Recipes = new ObservableCollection<Recipe>
            {
                new Recipe { Name = "Spaghetti Bolognese", Ingredients="Spaghetti, Tomato sauce", Description = "A classic Italian pasta dish with rich meat sauce." },
                new Recipe { Name = "Chicken Curry",  Ingredients= "Chicken, Curry", Description = "A flavorful curry dish with tender chicken pieces." },
                new Recipe { Name = "Vegetable Stir Fry", Ingredients="Vegetables, Pasta", Description = "A quick and healthy stir fry with fresh vegetables." }
            };
            WeakReferenceMessenger.Default.Register<DeleteRecipeMessage>(this, (r, m) =>
            {
                Recipes.Remove(m.Value);
            });
        }
        [RelayCommand]
        public async Task AddRecipeAsync()
        {
            var newRecipe = new Recipe { Name = "New Recipe", Ingredients="Ingredients here" ,Description = "Description here" };
            Recipes.Add(newRecipe);
            var param = new ShellNavigationQueryParameters
            {
                { "Recipe", newRecipe }
            };
            await Shell.Current.GoToAsync("editRecipePage", param);
            
        }
        [RelayCommand]
        public async Task FilterRecipeAsync()
        {
            var param = new ShellNavigationQueryParameters
            {
                { "AllRecipes", Recipes }
            };
            await Shell.Current.GoToAsync("filterPage", param);
        }

        [RelayCommand]
        public async Task RecipeSelectedAsync()
        {
            if (SelectedRecipe != null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "Recipe", SelectedRecipe }
                };
                await Shell.Current.GoToAsync("recipeDetailPage", param);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("No recipe selected");
            }
            SelectedRecipe = null;
        }
        
    }
}
