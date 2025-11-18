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

        //[ObservableProperty]
        //private Recipe editedRecipe;

        public MainPageViewModel()
        {
            Recipes = new ObservableCollection<Recipe>();
            Recipes = new ObservableCollection<Recipe>
            {
                new Recipe { Name = "Spaghetti Bolognese", Description = "A classic Italian pasta dish with rich meat sauce." },
                new Recipe { Name = "Chicken Curry", Description = "A flavorful curry dish with tender chicken pieces." },
                new Recipe { Name = "Vegetable Stir Fry", Description = "A quick and healthy stir fry with fresh vegetables." }
            };
        }
        [RelayCommand]
        public async Task AddRecipe()
        {
            await Shell.Current.GoToAsync("recipePage");
        }
        [RelayCommand]
        public async Task FilterRecipe()
        {
            await Shell.Current.GoToAsync("filterPage");
        }

        [RelayCommand]
        public async Task OnRecipeSelected()
        {
            if (SelectedRecipe != null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "Recipe", SelectedRecipe }
                };
                await Shell.Current.GoToAsync("recipePage", param);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("No recipe selected");
            }
        }
    }
}
