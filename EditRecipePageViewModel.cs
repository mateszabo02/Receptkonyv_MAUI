using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI
{
    [QueryProperty(nameof(EditedRecipe), "Recipe")]
    public partial class EditRecipePageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipe editedRecipe;

        [ObservableProperty]
        Recipe draft;
        partial void OnEditedRecipeChanged(Recipe value)
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
        public async Task SaveEdit()
        {
            if(Draft == null)
            {
                WeakReferenceMessenger.Default.Send("There is no recipe to edit!");
                return;
            }
            if (!string.IsNullOrWhiteSpace(Draft.Name) && Draft.Ingredients.Count()>0 && !string.IsNullOrWhiteSpace(Draft.Description))
            {
                EditedRecipe.Name = Draft.Name;
                EditedRecipe.Ingredients = new List<string>(Draft.Ingredients);
                EditedRecipe.Description = Draft.Description;
                await Shell.Current.GoToAsync("..");
            }   
            else
            {
                WeakReferenceMessenger.Default.Send("There is an empty unit!");
            }

        }
        [RelayCommand]
        public Task CancelEdit()
        {
            return Shell.Current.GoToAsync("..");
        }   
    }
}
