using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI.Models
{
    [Table("RecipeIngredient")]
    public class RecipeIngredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
    }
}
