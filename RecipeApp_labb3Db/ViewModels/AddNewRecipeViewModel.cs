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
        private readonly IDialogService dialogService;

        private string? _recipeName;
        public string? RecipeName
        {
            get => _recipeName;
            set
            {
                _recipeName = value;
                OnPropertyChanged();
            }
        }

        private string? _recipeDescription;

        public string? RecipeDescription
        {
            get => _recipeDescription;
            set
            {
                _recipeDescription = value;

            }
        }

        private ObservableCollection<Ingredient>? _recipeIngredients;
        public ObservableCollection<Ingredient>? RecipeIngredients
        {
            get => _recipeIngredients;
            set { _recipeIngredients = value; }
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
            set { _selectedUnit = value; }
        }

        private double _selectedAmount;

        public double SelectedAmount
        {
            get { return _selectedAmount; }
            set
            {
                _selectedAmount = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddIngredientToRecipeCommand { get; init; }
        public RelayCommand RemoveIngredientFromRecipeCommand { get; init; }
        public RelayCommand SaveRecipeCommand { get; init; }
        public RelayCommand ClearRecipeInputCommand { get; init; }

        public AddNewRecipeViewModel()
        {
            recipeDbAccess = new RecipeDataAccess();
            ingredientDataAccess = new IngredientDataAccess();

            RecipeIngredients = new ObservableCollection<Ingredient>();

            AddIngredientToRecipeCommand = new RelayCommand(AddIngredientToRecipe);
            RemoveIngredientFromRecipeCommand = new RelayCommand(RemoveIngredientFromRecipe);
            SaveRecipeCommand = new RelayCommand(SaveRecipe);
            ClearRecipeInputCommand = new RelayCommand(ClearRecipeInput);
            dialogService = new DialogService();
            LoadDataDb();
            loadUnits();
        }

        private void loadUnits()
        {
            var unitService = new UnitJsonService();
            var result = unitService.LoadUnits();
            Units = new ObservableCollection<Unit>(result);
        }

        public IngredientDataAccess ingredientDataAccess;
        private async void LoadDataDb()
        {
            var ingredients = await ingredientDataAccess.GetAllIngredients();
            IngredientsCollection = new ObservableCollection<Ingredient>(ingredients);
        }

        private void ClearRecipeInput(object obj)
        {
            bool ConfirmUndo = dialogService.ShowConfirmationDialog("You want to clear all inputed information", "Confirm");
            if (ConfirmUndo)
            {
                RecipeIngredients = new ObservableCollection<Ingredient>();
                RecipeDescription = string.Empty;
                RecipeName = string.Empty;
                OnPropertyChanged(nameof(RecipeDescription));
            }
        }

        private async void SaveRecipe(object obj)
        {
            bool confirmSave = dialogService.ShowConfirmationDialog("Do you want to save the recipe?", "Confirm");
            if (confirmSave)
            {
                var recipe = new Recipe
                {
                    Name = RecipeName,
                    Description = RecipeDescription,
                    Ingredients = RecipeIngredients.ToList()

                };

                await recipeDbAccess.CreateRecipe(recipe);

                RecipeName = string.Empty;
                RecipeDescription = string.Empty;
                RecipeIngredients = new ObservableCollection<Ingredient>();
            }

        }

        private void RemoveIngredientFromRecipe(object obj)
        {
            if (RecipeIngredients is null) return;

            RecipeIngredients.Remove(SelectedRecipeIngredient);

        }

        private void AddIngredientToRecipe(object obj)
        {
            if (SelectedIngredient != null)
            {

                var newIngredient = new Ingredient
                {
                    Name = SelectedIngredient.Name,
                    Category = SelectedIngredient.Category,

                    Unit = new Unit
                    {
                        UnitName = SelectedUnit.UnitName,
                        Amount = SelectedAmount

                    }
                };

                RecipeIngredients.Add(newIngredient);

                SelectedIngredient = null;
                SelectedUnit = null;
                SelectedAmount = 0;
            }


        }

    }
}
