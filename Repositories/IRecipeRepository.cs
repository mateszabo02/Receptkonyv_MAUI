using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI.Repositories
{
    public interface IRecipeRepository
    {
        Task InitializeAsync();
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task <List<Recipe>> GetRecipesByIngredientAsync(string ingredient);
        Task<int> AddRecipeAsync(Recipe recipe);
        Task<int> UpdateRecipeAsync(Recipe recipe);
        Task<int> DeleteRecipeAsync(int id);
    }
}
