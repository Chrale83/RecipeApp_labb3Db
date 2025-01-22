using RecipeApp_labb3Db.Presentation.Command;
using RecipeApp_labb3Db.Presentation.Services;
using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.Collections.ObjectModel;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    internal class RecipeListViewModel : ViewModelBase
    {
        private RecipeDataAccess? RecipeDataAccess;
        private ObservableCollection<Recipe>? _recipes;

        public ObservableCollection<Recipe>? Recipes
        {
            get => _recipes;
            set
            {
                _recipes = value;
                OnPropertyChanged();
            }
        }

        private Recipe? _selectedRecipe;

        public Recipe? SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
                DeleteRecipeCommand.RaiseCanExectueChanged();
            }
        }

        public RelayCommand DeleteRecipeCommand { get; }
        public RelayCommand EditRecipeCommand { get; }

        public RecipeListViewModel()
        {
            RecipeDataAccess = new RecipeDataAccess();
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe, CanDeleteRecipe);
                     
        }

        private bool CanDeleteRecipe(object? arg)
        {
            return SelectedRecipe != null ? true : false;
        }

        private async void DeleteRecipe(object obj)
        {
            var result = DialogService.ShowQuestionDialog($"Do you want to delete: {SelectedRecipe.Name}", "confirmation");
            if (result)
            {
                await RecipeDataAccess.DeleteRecipeFromDb(SelectedRecipe);
                DialogService.ShowConfirmationDialog($"{SelectedRecipe.Name} was deleted", "Recipe deleted");
                await GetAllRecipes();
            }
        }

        public async Task GetAllRecipes()
        {
            try
            {

                Recipes = new ObservableCollection<Recipe>(await RecipeDataAccess.GetAllRecipes());
                
                if (Recipes.Count == 0)
                {
                    var demoData = new JsonService();
                    Recipes = new ObservableCollection<Recipe>(demoData.LoadDemoRecipe());
                    RecipeDataAccess.CreateRecipe(Recipes.FirstOrDefault());
                    RecipeDataAccess.GetAllRecipes();
                }
            }
            catch (Exception e)
            {
                DialogService.ShowConfirmationDialog($"Fel inträffande {e.Message}", "Fel vid databas");
            }
        }
    }
}