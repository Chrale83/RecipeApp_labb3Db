
using RecipeApp_labb3Db.Presentation.Command;
using RecipeApp_labb3Db.Presentation.Services;
using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.Collections.ObjectModel;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    class AddNewIngredientViewModel : ViewModelBase
    {
        //Denna klassen ska visa lista på alla ingredienser som finns i databasen
        // Lägga till nya ingredienser
        // Redigera Ingredienser
        // Ta bort ingredienser
        //INgredienser ska innehålla namn och namn på kategori

        private ObservableCollection<Ingredient> _ingredients;

        public ObservableCollection<Ingredient> Ingredients
        {
            get => _ingredients;
            set { _ingredients = value; }
        }

        private Ingredient _selectedIngredient;

        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                IngredientName = SelectedIngredient.Name;
                IngredientCategory = SelectedIngredient.Category;
                OnPropertyChanged();
                
                
            }
        }

        private string _ingredientName;

        public string IngredientName
        {
            get => _ingredientName;
            set
            {
                _ingredientName = value;

                OnPropertyChanged();
                SaveIngredientCommand.RaiseCanExectueChanged();
              
            }
        }
        private string _ingredientCategory;

        public string IngredientCategory
        {
            get => _ingredientCategory;
            set
            {
                _ingredientCategory = value;
                OnPropertyChanged();
                SaveIngredientCommand.RaiseCanExectueChanged();
            }
        }

        public RelayCommand SaveIngredientCommand { get; set; }
        public RelayCommand RemoveInputCommand { get; }
        public AddNewIngredientViewModel()
        {
            SaveIngredientCommand = new RelayCommand(SaveIngredient, CanSaveIngredient);
            RemoveInputCommand = new RelayCommand(UndoInput);
        }

        private bool CanSaveIngredient(object? arg)
        {

            bool isNameInputed = !string.IsNullOrWhiteSpace(IngredientName);
            bool isCategoryInputed = !string.IsNullOrWhiteSpace(IngredientCategory);
            return isCategoryInputed && isCategoryInputed;
        }

        private async void SaveIngredient(object? arg)
        {
            var temptIng = new Ingredient { Name = IngredientName, Category = IngredientCategory };
            var validateDialog = new DialogService();
            var result = DialogService.ShowConfirmationDialog($"Do you want to add {temptIng.Name} in category {temptIng.Category}", "Add new ingredient");
            if (result)
            {
                var dataAccess = new IngredientDataAccess();
                await dataAccess.GetAndSetIngredient(temptIng);
            }
        }


        private void UndoInput(object? arg)
        {
            var dialog = new DialogService();
            bool result = dialog.ShowConfirmationDialog("Undo all inputed text", "Undo input");
            if (result)
            {
                SelectedIngredient.Name = string.Empty;
                SelectedIngredient.Category = string.Empty;
                OnPropertyChanged(nameof(SelectedIngredient));
            }
        }

        public async Task GetAllIngredients()
        {
            var db = new IngredientDataAccess();
            Ingredients = new ObservableCollection<Ingredient>(await db.GetAllIngredients());
        }



    }
}
