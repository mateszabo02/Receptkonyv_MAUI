using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Receptkonyv_MAUI
{
    [QueryProperty(nameof(EditedRecipe), "EditedRecipe")]
    public partial class MainPageViewModel : ObservableObject
    {
        //private string recipeDbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "recipes.db3");
        //private SQLiteAsyncConnection recipeDb;


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
        public Recipe EditedRecipe
        {
            set
            {
                if(value!= null)
                {
                    if(SelectedRecipe != null)
                    {
                        Recipes.Remove(SelectedRecipe);
                        SelectedRecipe = null;
                    }
                    Recipes.Add(value);
                }
            }
        }

        public MainPageViewModel()
        {
            Recipes = new ObservableCollection<Recipe>();
            Recipes = new ObservableCollection<Recipe>
            {
                new Recipe { Name = "Spaghetti Bolognese", Ingredients= new ObservableCollection<Ingredient>{ new Ingredient { Name = "Tomato"},  new Ingredient { Name = "Spaghetti"}}, Description = "A classic Italian pasta dish with rich meat sauce." },
                new Recipe { Name = "Chicken Curry",  Ingredients= new ObservableCollection<Ingredient>{ new Ingredient { Name = "Chicken"},  new Ingredient { Name = "Curry"}}, Description = "A flavorful curry dish with tender chicken pieces." },
                new Recipe { Name = "Vegetable Stir Fry", Ingredients= new ObservableCollection<Ingredient>{ new Ingredient { Name = "Vegetables"},  new Ingredient { Name = "Pasta"}}, Description = "A quick and healthy stir fry with fresh vegetables." }
            };
            WeakReferenceMessenger.Default.Register<DeleteRecipeMessage>(this, (r, m) =>
            {
                Recipes.Remove(m.Value);
            });



        }
        [RelayCommand]
        public async Task AddRecipeAsync()
        {
            SelectedRecipe = null;

            var param = new ShellNavigationQueryParameters
            {
                { "Recipe", new Recipe() }
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
