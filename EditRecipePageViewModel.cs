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
            if (!string.IsNullOrWhiteSpace(Draft.Name) && !string.IsNullOrWhiteSpace(Draft.Description) && !string.IsNullOrWhiteSpace(Draft.Ingredients)){
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
        [RelayCommand]
        public async Task OpenImageAsync()
        {
            var image = await MediaPicker.Default.PickPhotoAsync();
            if(image != null)
            {
                string localUrl = Path.Combine(FileSystem.Current.AppDataDirectory, Draft.Id+'_'+image.FileName);
                if(!File.Exists(localUrl))
                {
                    using Stream stream = await image.OpenReadAsync();
                    using FileStream fs = File.OpenWrite(localUrl);
                    await stream.CopyToAsync(fs);
                }
                Draft.ImageUrl = localUrl;
            }
        }
        [RelayCommand]
        async Task TakePhotoAsync()
        {
            if (!MediaPicker.Default.IsCaptureSupported)
            {
                return;
            }
            var image = await MediaPicker.Default.CapturePhotoAsync();
            if (image != null)
            {
                string localUrl = Path.Combine(FileSystem.Current.AppDataDirectory, image.FileName);
                if (!File.Exists(localUrl))
                {
                    using Stream stream = await image.OpenReadAsync();
                    using FileStream fs = File.OpenWrite(localUrl);
                    await stream.CopyToAsync(fs);
                }
                Draft.ImageUrl = localUrl;
            }
        }
    }
}
