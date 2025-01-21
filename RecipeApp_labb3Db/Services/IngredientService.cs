using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;

namespace RecipeApp_labb3Db.Presentation.Services
{
    class IngredientService
    {
        private readonly IngredientDataAccess ingredientDataAccess = new();

        public async Task UpdateIngredientService(Ingredient updatedIngredient, string NewName, string NewCategory)
        {
            var tempOldName = NewName;
            var tempOldCategory = NewCategory;
            updatedIngredient.Name = NewName;
            updatedIngredient.Category = NewCategory;
            await ingredientDataAccess.UpdateIngredientToDb(updatedIngredient);
            DialogService.ShowConfirmationDialog($"{tempOldName} {tempOldCategory}\n updated to \n {updatedIngredient.Name} {updatedIngredient.Category}", "Updated");
        }
        
        public async Task<bool> DeleteIngredientService(string ingredientName)
        {
            try
            {
                var dataAccess = new IngredientDataAccess();
                var result = await ingredientDataAccess.DeleteIngredientFromDb(ingredientName);
                if (result)
                {
                    DialogService.ShowConfirmationDialog($"{ingredientName} was deleted from database", "deleted ingredient");
                    return true;
                }
                else
                {
                    DialogService.ShowConfirmationDialog($"{ingredientName} didnt exits in the database", "error ingredient delete");
                    return false;
                }
            }
            catch (Exception e)
            {
                DialogService.ShowConfirmationDialog($"{e.Message}", "error");
                return false;
            }
        }
    }
}
