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

        public RecipeListViewModel()
        {
            
        }
        public async Task GetAllRecipes()
        {
            RecipeDataAccess = new RecipeDataAccess();
            Recipes = new ObservableCollection<Recipe>(await RecipeDataAccess.GetAllRecipes());
        }
    }
}