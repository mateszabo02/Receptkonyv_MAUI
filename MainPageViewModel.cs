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
                new Recipe { Name = "Spaghetti Bolognese", Ingredients=new List<string>(){"Spaghetti", "Tomato sauce" },Description = "A classic Italian pasta dish with rich meat sauce." },
                new Recipe { Name = "Chicken Curry",  Ingredients=new List<string>(){"Chicken", "Curry"},Description = "A flavorful curry dish with tender chicken pieces." },
                new Recipe { Name = "Vegetable Stir Fry",  Ingredients=new List<string>(){"Vegetables", "Pasta"},Description = "A quick and healthy stir fry with fresh vegetables." }
            };
        }
        [RelayCommand]
        public async Task AddRecipe()
        {
            await Shell.Current.GoToAsync("recipeDetailPage");
        }
        [RelayCommand]
        public async Task FilterRecipe()
        {
            await Shell.Current.GoToAsync("filterPage");
        }

        [RelayCommand]
        public async Task RecipeSelected()
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
