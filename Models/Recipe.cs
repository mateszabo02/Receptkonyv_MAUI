using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI
{
    [Table("Recipe")]
    public partial class Recipe : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(200), NotNull]
        public string Name { get; set; } = string.Empty;

        [NotNull]
        public string Description { get; set; } = string.Empty;

        [Ignore]
        public ObservableCollection<Ingredient> Ingredients { get; set; } = new();

        [Ignore]
        public string IngredientsString{ get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

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
