using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI
{
    public partial class Ingredient : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        int receptId;
        public override string ToString()
        {
            return Name;
        }
    }
}
