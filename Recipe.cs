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
        string ingredients;

        [ObservableProperty]
        string imageUrl;
        public Recipe GetCopy()
        {
            Id=this.Id;
            return (Recipe)this.MemberwiseClone();
        }
        public override string ToString()
        {
            return $"Name: {Name}\nIngredients: {Ingredients}\nDescription: {Description}";
        }
    }
}
