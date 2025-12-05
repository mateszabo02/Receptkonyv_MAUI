using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Receptkonyv_MAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI
{
    [QueryProperty(nameof(EditedRecipe), "Recipe")]
    [QueryProperty(nameof(IsNew), "IsNew")]
    public partial class EditRecipePageViewModel : ObservableObject
    {
        public EditRecipePageViewModel(IRecipeRepository repository)
        {
            _repository = repository;
        }
        private readonly IRecipeRepository _repository;
        [ObservableProperty]
        Recipe editedRecipe;

        [ObservableProperty]
        Recipe draft;

        [ObservableProperty]
        bool isNew;
        partial void OnEditedRecipeChanged(Recipe value)
        {
            InitDraft(value);
            Draft.UpdateStrings();
        }
        public void InitDraft(Recipe value)
        {
            Draft = value.GetCopy();
        }
        [RelayCommand]
        public async Task AddIngredientAsync()
        {
            if(Draft.Ingredients == null)
            {
                Draft.Ingredients = new ObservableCollection<Ingredient>();
            }
            
            Draft.Ingredients.Add(new Ingredient() { Name = "" });
            Draft.UpdateStrings();
        }
        [RelayCommand]
        public async Task RemoveIngredientAsync(Ingredient item)
        {
            if(Draft.Ingredients.Contains(item))
            {
                Draft.Ingredients.Remove(item);
                Draft.UpdateStrings();
            }
        }
        [RelayCommand]
        public async Task SaveEditAsync()
        {
            if (!string.IsNullOrWhiteSpace(Draft.Name) && !string.IsNullOrWhiteSpace(Draft.Description) && Draft.Ingredients.Count()>0 && Draft.Ingredients.All(i => !string.IsNullOrWhiteSpace(i.Name)))
            {
                if (IsNew)
                {
                    await _repository.AddRecipeAsync(Draft);
                }
                else{
                    await _repository.UpdateRecipeAsync(Draft);
                }
                var param = new ShellNavigationQueryParameters
                {
                    { "EditedRecipe", Draft }
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
