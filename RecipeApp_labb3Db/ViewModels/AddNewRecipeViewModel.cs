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
        public IngredientDataAccess ingredientDataAccess;
        private bool isEditingRecipe = false;

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
                OnPropertyChanged();
                SaveRecipeCommand.RaiseCanExectueChanged();
            }
        }

        private ObservableCollection<Ingredient>? _ingredientsCollection;

        public ObservableCollection<Ingredient>? IngredientsCollection
        {
            get => _ingredientsCollection;
            set
            {
                _ingredientsCollection = value;
                OnPropertyChanged();
            }
        }

        private Ingredient _selectedIngredient;

        public Ingredient? SelectedIngredient
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

        private ObservableCollection<Recipe> _recipes;

        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            set
            {
                _recipes = value;
                OnPropertyChanged();
            }
        }

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
                EditRecipeCommand.RaiseCanExectueChanged();
            }
        }


        public RelayCommand AddIngredientToRecipeCommand { get; init; }
        public RelayCommand RemoveIngredientFromRecipeCommand { get; init; }
        public RelayCommand SaveRecipeCommand { get; init; }
        public RelayCommand ClearRecipeInputCommand { get; init; }
        public RelayCommand EditRecipeCommand { get; }

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
            EditRecipeCommand = new RelayCommand(EditRecipe, CanEditRecipe);
            LoadUnits();
            _ = LoadDataStartup();
        }

        private bool CanEditRecipe(object? arg)
        {
            return SelectedRecipe != null;
        }

        private void EditRecipe(object obj)
        {
            isEditingRecipe = true;
            RecipeName = SelectedRecipe.Name;
            RecipeDescription = SelectedRecipe.Description;
            RecipeIngredients = new ObservableCollection<Ingredient>(SelectedRecipe.Ingredients);
        }

        public async Task GetRecipes()
        {
            Recipes = new ObservableCollection<Recipe>(await recipeDbAccess.GetAllRecipes());
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

        private void LoadUnits()
        {
            var unitService = new JsonService();
            var result = unitService.LoadUnits();
            Units = new ObservableCollection<Unit>(result);
        }


        private async Task LoadDataStartup()
        {
            try
            {
                var ingredients = await ingredientDataAccess.GetAllIngredientsFromDb();
                if (ingredients.Count != 0)
                {
                    IngredientsCollection = new ObservableCollection<Ingredient>(ingredients);
                }
                else
                {
                    var unitService = new JsonService();
                    ingredients = unitService.LoadIngredient();
                    await ingredientDataAccess.SetAllIngredientsToDB(ingredients);
                    IngredientsCollection = new ObservableCollection<Ingredient>(ingredients);
                    //OnPropertyChanged(nameof(IngredientsCollection));
                }
                await GetRecipes();

            }
            catch (Exception e)
            {

                DialogService.ShowConfirmationDialog($"Fel vid databas {e.Message}", "fel vid databas");
            }
        }

        public async Task GetAllIngredients()
        {
            
            IngredientsCollection = new ObservableCollection<Ingredient>(await ingredientDataAccess.GetAllIngredientsFromDb());
        }
        private void ClearRecipeInput(object obj)
        {
            bool ConfirmUndo = DialogService.ShowQuestionDialog("You want to clear all inputed information", "Confirm");
            if (ConfirmUndo)
            {
                isEditingRecipe = false;
                ClearRecipeInput();
                ClearIngredientInput();
            }
        }

        private async void SaveRecipe(object obj)
        {
            if (isEditingRecipe)
            {
                bool confirmEdit = DialogService.ShowQuestionDialog("Do you want to update the recipe?", "Confirm");
                if (confirmEdit)
                {
                    await recipeService.UpdateRecipe(RecipeName, RecipeDescription, RecipeIngredients, SelectedRecipe.Id);
                }
            }
            else
            {
                await recipeService.SaveRecipe(RecipeName, RecipeDescription, RecipeIngredients);
            }
                ClearRecipeInput();
        }
        public async Task GetAllRecipes()
        {
            Recipes = new ObservableCollection<Recipe>(await recipeDbAccess.GetAllRecipes());
            
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
                RecipeIngredients?.Add(newIngredient);
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
        }

        private void ClearIngredientInput()
        {
            SelectedIngredient = null;
            SelectedAmount = 0;
        }
    }
}