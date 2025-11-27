using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI
{
    [QueryProperty(nameof(AllRecipes), "AllRecipes")]
    public partial class FilterPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string filterTerm;

        private List<Recipe> source = new();

        [ObservableProperty]
        private ObservableCollection<Recipe> filteredRecipes;

        public ObservableCollection<Recipe> AllRecipes
        {
            set
            {
                if (value != null)
                {
                    source = new List<Recipe>(value);
                    FilteredRecipes = new ObservableCollection<Recipe>(source);
                    foreach(var i in FilteredRecipes)
                    {
                        i.UpdateStrings();
                    }
                }
            }
        }

        partial void OnFilterTermChanged(string value)
        {
            ApplyFilter();
        }

        [RelayCommand]
        public async Task ApplyFilterAsync()
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (source == null)
                return;

            if (string.IsNullOrWhiteSpace(FilterTerm))
            {
                FilteredRecipes = new ObservableCollection<Recipe>(source);
                return;
            }

            var term = FilterTerm.ToLower();

            List<Recipe> filtered = new();
            foreach (var recipe in source)
            {
                if (recipe.Ingredients != null && recipe.Ingredients.Any(i=>i.Name.ToLower().Contains(term)))
                {
                    filtered.Add(recipe);
                }
            }

            FilteredRecipes = new ObservableCollection<Recipe>(filtered);
        }
    }
}