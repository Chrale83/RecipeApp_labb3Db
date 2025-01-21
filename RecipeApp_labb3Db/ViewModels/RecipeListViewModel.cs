using RecipeApp_labb3Db.Presentation.Services;
using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.Collections.ObjectModel;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    internal class RecipeListViewModel : ViewModelBase
    {
        public RecipeDataAccess RecipeDataAccess;
        private ObservableCollection<Recipe> _recipes;

        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set { _recipes = value; }
        }

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
            }
        }

        


        public async Task GetAllRecipes()
        {
            try
            {
                RecipeDataAccess = new RecipeDataAccess();
                Recipes = new ObservableCollection<Recipe>(await RecipeDataAccess.GetAllRecipes());
            }
            catch (Exception e)
            {
                DialogService.ShowConfirmationDialog($"Fel inträffande {e.Message}", "Fel vid databas");
            }
        }
    }
}
