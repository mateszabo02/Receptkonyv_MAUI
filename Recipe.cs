using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI
{
    public partial class Recipe : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        private ObservableCollection<Ingredient> ingredients = new();

        [ObservableProperty]
        string ingredientsString;

        [ObservableProperty]
        string imageUrl;
        public Recipe GetCopy()
        {
            Id=this.Id;
            return (Recipe)this.MemberwiseClone();
        }
        public void UpdateStrings()
        {
            IngredientsString = "";
            foreach(var i in Ingredients)
            {
                IngredientsString += i.ToString() + ", ";
            }
        }
        public override string ToString()
        {
            return $"Name: {Name}\nIngredients: {IngredientsString}\nDescription: {Description}";
        }
    }
}
