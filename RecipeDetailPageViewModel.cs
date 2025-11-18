using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//[QueryProperty(nameof(SelectPet), "SelectPet")]
namespace Receptkonyv_MAUI
{
    [QueryProperty(nameof(editedRecipe), "Recipe")]
    public partial class RecipeDetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipe editedRecipe;
        [ObservableProperty]
        Recipe draft;

        public void InitDraft()
        {
            Draft = editedRecipe.GetCopy();
        }
        [RelayCommand]
        public async Task SaveRecipe()
        {
            var param = new ShellNavigationQueryParameters
            {
                { "SelectedRecipe", Draft }
            };
            await Shell.Current.GoToAsync("..", param);
        }
        
    }
}
