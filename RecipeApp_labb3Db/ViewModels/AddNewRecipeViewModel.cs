using RecipeApp_labb3Db.Presentation.Command;
using RecipeApp_labb3Db.Presentation.Services;
using RecipeDbAccess.DataAccess;
using RecipeDbAccess.Models;
using System.Collections.ObjectModel;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    internal class AddNewRecipeViewModel : ViewModelBase
    {
        public RecipeDataAccess recipeDbAccess;
        private readonly DialogService dialogService;
        public RecipeService recipeService;

        private string? _recipeName;
        public string? RecipeName
        {
            get => _recipeName;
            set
            {
                _recipeName = value;
                OnPropertyChanged();
                SaveRecipeCommand.RaiseCanExectueChanged();
            }
        }

        private string? _recipeDescription;

        public string? RecipeDescription
        {
            get => _recipeDescription;
            set
            {
                _recipeDescription = value;
                OnPropertyChanged();
                SaveRecipeCommand.RaiseCanExectueChanged();
            }
        }

        private ObservableCollection<Ingredient>? _recipeIngredients;
        public ObservableCollection<Ingredient>? RecipeIngredients
        {
            get => _recipeIngredients;
            set
            {
                _recipeIngredients = value;
                SaveRecipeCommand.RaiseCanExectueChanged();
            }
        }

        private ObservableCollection<Ingredient> _ingredientsCollection;

        public ObservableCollection<Ingredient> IngredientsCollection
        {
            get => _ingredientsCollection;
            set
            {
                _ingredientsCollection = value;
            }
        }

        private Ingredient _selectedIngredient;

        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                OnPropertyChanged();
                AddIngredientToRecipeCommand.RaiseCanExectueChanged();

            }
        }
        private Ingredient _selectedRecipeIngredient;

        public Ingredient SelectedRecipeIngredient
        {
            get => _selectedRecipeIngredient;
            set { _selectedRecipeIngredient = value; }
        }

        public ObservableCollection<Unit> _units;
        public ObservableCollection<Unit> Units
        {
            get => _units;
            set { _units = value; }
        }

        private Unit _selectedUnit;

        public Unit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                AddIngredientToRecipeCommand.RaiseCanExectueChanged();
            }
        }

        private double _selectedAmount;

        public double SelectedAmount
        {
            get { return _selectedAmount; }
            set
            {
                _selectedAmount = value;
                OnPropertyChanged();
                AddIngredientToRecipeCommand.RaiseCanExectueChanged();
            }
        }
        public RelayCommand AddIngredientToRecipeCommand { get; init; }
        public RelayCommand RemoveIngredientFromRecipeCommand { get; init; }
        public RelayCommand SaveRecipeCommand { get; init; }
        public RelayCommand ClearRecipeInputCommand { get; init; }

        public AddNewRecipeViewModel()
        {
            SaveRecipeCommand = new RelayCommand(SaveRecipe, CanSaveRecipe);
            recipeService = new RecipeService();
            recipeDbAccess = new RecipeDataAccess();
            ingredientDataAccess = new IngredientDataAccess();
            RecipeIngredients = new ObservableCollection<Ingredient>();
            AddIngredientToRecipeCommand = new RelayCommand(AddIngredientToRecipe, CanAddIngredientToRecipe);
            RemoveIngredientFromRecipeCommand = new RelayCommand(RemoveIngredientFromRecipe);
            ClearRecipeInputCommand = new RelayCommand(ClearRecipeInput);
            dialogService = new DialogService();
            LoadDataDb();
            loadUnits();
        }

        private bool CanAddIngredientToRecipe(object? arg)
        {
            bool isIngredientSelected = SelectedIngredient != null;
            bool isAmountSelected = SelectedAmount > 0;
            bool isUnitSelected = SelectedUnit != null;
            return isIngredientSelected && isAmountSelected && isUnitSelected;
        }

        private bool CanSaveRecipe(object? arg)
        {
            bool isNameEmpty = !string.IsNullOrWhiteSpace(RecipeName);
            bool isDescriptionEmpty = !string.IsNullOrWhiteSpace(RecipeDescription);
            bool isIngredientlistEmpty = RecipeIngredients.Any();

            return isNameEmpty && isDescriptionEmpty && isIngredientlistEmpty;
        }

        private void loadUnits()
        {
            var unitService = new UnitJsonService();
            var result = unitService.LoadUnits();
            Units = new ObservableCollection<Unit>(result);
        }

        public IngredientDataAccess ingredientDataAccess;
        private async Task LoadDataDb()
        {
            var ingredients = await ingredientDataAccess.GetAllIngredients();
            if (ingredients.Count != 0)
            {
                IngredientsCollection = new ObservableCollection<Ingredient>(ingredients);
            }
            else
            {
                var unitService = new UnitJsonService();
                ingredients = unitService.LoadIngredient();
                await ingredientDataAccess.SetAllIngredients(ingredients);
                IngredientsCollection = new ObservableCollection<Ingredient>(ingredients);
                OnPropertyChanged(nameof(IngredientsCollection));
            }
        }
        private void ClearRecipeInput(object obj)
        {
            bool ConfirmUndo = dialogService.ShowConfirmationDialog("You want to clear all inputed information", "Confirm");
            if (ConfirmUndo)
            {
                ClearRecipeInput();
                ClearIngredientInput();
            }
        }

        private async void SaveRecipe(object obj)
        {
            bool confirmSave = dialogService.ShowConfirmationDialog("Do you want to save the recipe?", "Confirm");
            if (confirmSave)
            {
                await recipeService.SaveRecipe(RecipeName, RecipeDescription, RecipeIngredients);
                ClearRecipeInput();
            }
        }

        private void RemoveIngredientFromRecipe(object obj)
        {
            if (RecipeIngredients != null)
            {
                RecipeIngredients.Remove(SelectedRecipeIngredient);
                SaveRecipeCommand.RaiseCanExectueChanged();
            }
        }

        private void AddIngredientToRecipe(object obj)
        {
            if (SelectedIngredient != null)
            {
                var newIngredient = recipeService.AddIngredientToRecipe(SelectedIngredient.Name, SelectedIngredient.Category, SelectedUnit.UnitName, SelectedAmount);
                RecipeIngredients.Add(newIngredient);
                ClearIngredientInput();
                OnPropertyChanged(nameof(RecipeIngredients));
                SaveRecipeCommand.RaiseCanExectueChanged();
            }
        }
        private void ClearRecipeInput()
        {
            RecipeName = string.Empty;
            RecipeDescription = string.Empty;
            RecipeIngredients = new ObservableCollection<Ingredient>();
            //OnPropertyChanged(nameof(RecipeDescription));
        }

        private void ClearIngredientInput()
        {
            SelectedIngredient = null;
            SelectedAmount = 0;
        }
    }
}