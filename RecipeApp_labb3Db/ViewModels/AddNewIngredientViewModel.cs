
using MongoDB.Bson;
using RecipeApp_labb3Db.Presentation.Command;
using RecipeApp_labb3Db.Presentation.Services;
using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.CodeDom;
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

        private Ingredient? _selectedIngredient;

        public Ingredient? SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                if (value is null)
                {
                    return;
                }
                IngredientName = SelectedIngredient.Name;
                IngredientCategory = SelectedIngredient.Category;
                OnPropertyChanged();
                UpdateIngredientCommand.RaiseCanExectueChanged();
            }
        }

        private string? _ingredientName;

        public string? IngredientName
        {
            get => _ingredientName;
            set
            {
                _ingredientName = value;
                OnPropertyChanged();
                SaveIngredientCommand.RaiseCanExectueChanged();
                DeleteIngredientCommand.RaiseCanExectueChanged();
                UpdateIngredientCommand.RaiseCanExectueChanged();

            }
        }
        private string? _ingredientCategory;

        public string? IngredientCategory
        {
            get => _ingredientCategory;
            set
            {
                _ingredientCategory = value;
                OnPropertyChanged();
                SaveIngredientCommand.RaiseCanExectueChanged();
                DeleteIngredientCommand.RaiseCanExectueChanged();
                UpdateIngredientCommand.RaiseCanExectueChanged();
            }
        }

        public RelayCommand SaveIngredientCommand { get; set; }
        public RelayCommand RemoveInputCommand { get; }
        public RelayCommand DeleteIngredientCommand { get; }
        public RelayCommand UpdateIngredientCommand { get; }
        public AddNewIngredientViewModel()
        {
            SaveIngredientCommand = new RelayCommand(SaveIngredient, CanSaveIngredient);
            RemoveInputCommand = new RelayCommand(ClearInputedFields);
            DeleteIngredientCommand = new RelayCommand(DeleteIngredient, CanDeleteIngredient);
            UpdateIngredientCommand = new RelayCommand(UpdateIngredient, CanUpdateIngredient);
        }

        private bool CanUpdateIngredient(object? arg)
        {
            bool isIngredientNameValid = IngredientName != string.Empty;
            bool isCategoryEmpty = IngredientCategory != string.Empty;
            return SelectedIngredient != null;
        }

        private async void UpdateIngredient(object obj)
        {
            var result = DialogService.ShowQuestionDialog($"Do you want to update {SelectedIngredient.Name} to {IngredientName}", "Update");
            if (result)
            {
                var ingredientService = new IngredientService();
                await ingredientService.UpdateIngredientService(SelectedIngredient, IngredientName, IngredientCategory);
                
            }
        }
                



        private bool CanDeleteIngredient(object? arg)
        {
            bool isNameInputed = !string.IsNullOrWhiteSpace(IngredientName);
            bool isCategoryInputed = !string.IsNullOrWhiteSpace(IngredientCategory);
            return isCategoryInputed && isNameInputed;
        }

        private async void DeleteIngredient(object obj)
        {
            var result = DialogService.ShowQuestionDialog($"Do you want to delete {IngredientName}", "Delete");
            if (result)
            {
                var ingredientService = new IngredientService();
                await ingredientService.DeleteIngredientService(IngredientName);
                ClearInputedFields();
                await GetAllIngredients();
            }
        }


        private bool CanSaveIngredient(object? arg)
        {
            bool isNameInputed = !string.IsNullOrWhiteSpace(IngredientName);
            bool isCategoryInputed = !string.IsNullOrWhiteSpace(IngredientCategory);
            return isCategoryInputed && isNameInputed;
        }

        private async void SaveIngredient(object? arg)
        {
            var temptIng = new Ingredient { Name = IngredientName, Category = IngredientCategory };

            var inputChoice = DialogService.ShowQuestionDialog($"Do you want to add {temptIng.Name} in category {temptIng.Category}", "Add new ingredient");
            if (inputChoice)
            {
                var dataAccess = new IngredientDataAccess();
                var result = await dataAccess.GetAndSetIngredientFromDB(temptIng);
                if (result)
                {
                    DialogService.ShowConfirmationDialog($"{temptIng.Name} added to ingredients", "added ingredients succesfull");
                }
            }
        }

        private void ClearInputedFields(object? arg)
        {
            bool result = DialogService.ShowQuestionDialog("Undo all inputed text", "Undo input");
            if (result)
            {
                SelectedIngredient.Name = string.Empty;
                SelectedIngredient.Category = string.Empty;
                IngredientName = string.Empty;
                IngredientCategory = string.Empty;
                OnPropertyChanged(nameof(SelectedIngredient));
            }
        }

        private void ClearInputedFields()
        {
            //SelectedIngredient.Name = string.Empty;
            //SelectedIngredient.Category = string.Empty;
            IngredientName = string.Empty;
            IngredientCategory = string.Empty;
            OnPropertyChanged(nameof(SelectedIngredient));
        }

        public async Task GetAllIngredients()
        {
            try
            {
                var db = new IngredientDataAccess();
                Ingredients = new ObservableCollection<Ingredient>(await db.GetAllIngredientsFromDb());
                OnPropertyChanged(nameof(Ingredients));
                //SelectedIngredient = new Ingredient();
            }
            catch (Exception e)
            {
                DialogService.ShowConfirmationDialog($"Fel vid databas {e.Message}", "fel vid databas");
            }
        }

    }
}

