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

        private string _testString;

        public string TestString
        {
            get { return _testString; }
            set
            {
                _testString = value;
                OnPropertyChanged();
            }
        }


        public RecipeListViewModel()
        {
            TestString = "Data";
            GetAllRecipes();
        }

        private async void GetAllRecipes()
        {
            RecipeDataAccess = new RecipeDataAccess();
            //var recipes = await RecipeDataAccess.GetAllRecipes();
            //Recipes = new ObservableCollection<Recipe>(recipes);
            Recipes = new ObservableCollection<Recipe>(await RecipeDataAccess.GetAllRecipes());

        }
    }
}
