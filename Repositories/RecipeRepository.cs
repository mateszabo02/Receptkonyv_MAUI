using Receptkonyv_MAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptkonyv_MAUI.Repositories
{
    internal class RecipeRepository : IRecipeRepository
    {
        private readonly SQLiteAsyncConnection _db;
        public RecipeRepository()
        {
            var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "recipes.db3");
            _db = new SQLiteAsyncConnection(dbPath);
        }
        public async Task<int> AddRecipeAsync(Recipe recipe)
        {
            await InitializeAsync();
            //először maga a recept
            recipe.IngredientsString = string.Join(", ", recipe.Ingredients.Select(i => i.Name));
            await _db.InsertAsync(recipe);

            //majd az összetevők
            await SaveIngredientsForRecipeAsync(recipe);
            return recipe.Id;
        }

        private async Task SaveIngredientsForRecipeAsync(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                Ingredient dbIngredient;
                if(ingredient.Id == 0)
                {
                    //név alapján keresek, ha nem akarok duplikált nevet
                    dbIngredient = new Ingredient { Name = ingredient.Name.Trim() };
                    await _db.InsertAsync(dbIngredient);
                }
                else
                {
                    dbIngredient = ingredient;
                }
                var link = new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = dbIngredient.Id
                };
                await _db.InsertAsync(link);
            }
        }

        public async Task<int> DeleteRecipeAsync(int id)
        {
            await InitializeAsync();
            //kapcsolatok törlése
            var links = await _db.Table<RecipeIngredient>()
                            .Where(ri => ri.RecipeId == id)
                            .ToListAsync();
            foreach (var link in links)
            {
                await _db.DeleteAsync(link);
            }
            //recept törlése
            return await _db.DeleteAsync<Recipe>(id);
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            await InitializeAsync();
            var recipes = await _db.Table<Recipe>()
                            .OrderBy(r=> r.Name)
                            .ToListAsync();

            //hozzávalók betöltése mindegyikhez
            foreach(var recipe in recipes)
            {
                await LoadIngredientsForRecipeAsync(recipe);
            }
            return recipes;
        }

        private async Task LoadIngredientsForRecipeAsync(Recipe recipe)
        {
            var links = await _db.Table<RecipeIngredient>()
                            .Where(ri => ri.RecipeId == recipe.Id)
                            .ToListAsync();
            var ingredientIds = links.Select(ri => ri.IngredientId).ToList();
            if(ingredientIds.Count == 0)
            {
                recipe.Ingredients.Clear();
                return;
            }
            var ingredients = await _db.Table<Ingredient>()
                                    .Where(i => ingredientIds.Contains(i.Id))
                                    .ToListAsync();
            recipe.Ingredients.Clear();
            foreach (var ingredient in ingredients)
            {
                recipe.Ingredients.Add(ingredient);
            }
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            await InitializeAsync();
            var recipe =  await _db.Table<Recipe>()
                            .Where(r => r.Id == id)
                            .FirstOrDefaultAsync();
            if(recipe is null)
            {
                return null;
            }
            await LoadIngredientsForRecipeAsync(recipe);
            return recipe;
        }

        public async Task<List<Recipe>> GetRecipesByIngredientAsync(string ingredient)
        {
            await InitializeAsync();
            ingredient = ingredient.Trim().ToLower();
            if(string.IsNullOrWhiteSpace(ingredient))
            {
                return await GetAllRecipesAsync();
            }
            var ingredients = await _db.Table<Ingredient>()
                                .Where(i => i.Name.ToLower().Contains(ingredient))
                                .ToListAsync();
            if(!ingredients.Any())
            {
                return new List<Recipe>();
            }
            var ingredientIds = ingredients.Select(i => i.Id).ToList();
            var links = await _db.Table<RecipeIngredient>()
                            .Where(ri => ingredientIds.Contains(ri.IngredientId))
                            .ToListAsync();
            var recipeIds = links.Select(ri => ri.RecipeId).Distinct().ToList();
            var recipes = await _db.Table<Recipe>()
                            .Where(r => recipeIds.Contains(r.Id))
                            .OrderBy(r => r.Name)
                            .ToListAsync();
            foreach (var recipe in recipes)
            {
                await LoadIngredientsForRecipeAsync(recipe);
            }
            return recipes;
        }

        public async Task InitializeAsync()
        {
            await _db.CreateTableAsync<Recipe>();
            await _db.CreateTableAsync<Ingredient>();
            await _db.CreateTableAsync<RecipeIngredient>();
        }

        public async Task<int> UpdateRecipeAsync(Recipe recipe)
        {
            await InitializeAsync();
            recipe.IngredientsString = string.Join(", ", recipe.Ingredients.Select(i => i.Name));
            await _db.UpdateAsync(recipe);

            //régi kapcsolatok törlése
            var oldLinks = await _db.Table<RecipeIngredient>()
                                .Where(ri => ri.RecipeId == recipe.Id)
                                .ToListAsync();
            foreach (var link in oldLinks)
                await _db.DeleteAsync(link);
            //új kapcsolatok mentése
            await SaveIngredientsForRecipeAsync(recipe);
            return recipe.Id;
        }
    }
}
