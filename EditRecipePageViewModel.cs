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
            Draft = value.GetCopy();
        }
        [RelayCommand]
        public async Task SaveEdit()
        {
            if (!string.IsNullOrWhiteSpace(Draft.Name) && !string.IsNullOrWhiteSpace(Draft.Description) ){
                var param = new ShellNavigationQueryParameters
            {
                { "editedRecipe", Draft }
            };
                await Shell.Current.GoToAsync("..", param);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("All entries must have a value");
            }
            
        }
        [RelayCommand]
        public Task CancelEdit()
        {
            return Shell.Current.GoToAsync("..");
        }   
    }
}
