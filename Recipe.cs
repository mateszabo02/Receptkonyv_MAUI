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
        ObservableCollection<string> ingredients;

        [ObservableProperty]
        string pictureUrl;
        public Recipe GetCopy()
        {
            return (Recipe)this.MemberwiseClone();
        }
    }
}
