
using RecipeApp_labb3Db.Presentation.Command;
using RecipeDbAccess.DataAccess;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public TopMenuViewModel TopMenuViewModel { get; }
        public RecipeMenuViewModel RecipeMenuViewModel { get; }
        public AddNewIngredientViewModel AddNewIngredientViewModel { get; }

        private ViewModelBase _SelectedView;

        public ViewModelBase SelectedView
        {
            get => _SelectedView;
            set
            {
                _SelectedView = value;
                OnPropertyChanged();
            }
        }
        
        public RelayCommand SwapToRecipeViewCommand { get; }
        public RelayCommand SwapToIngredientViewCommand { get; }
        public MainViewModel()
        {
            TopMenuViewModel = new TopMenuViewModel();
            RecipeMenuViewModel = new RecipeMenuViewModel();
            AddNewIngredientViewModel = new AddNewIngredientViewModel();
            SelectedView = RecipeMenuViewModel;
            SwapToIngredientViewCommand = new RelayCommand(SwapToIngredientView);
            SwapToRecipeViewCommand = new RelayCommand(SwapToRecipeView);
        }

        private async void SwapToRecipeView(object? arg)
        {
            await RecipeMenuViewModel.AddNewRecipeViewModel.GetAllIngredients();
            SelectedView = RecipeMenuViewModel;
        }

        private async void SwapToIngredientView(object? arg)
        {
            await GetAllIngredients();
            SelectedView = AddNewIngredientViewModel;
        }

        public async Task GetAllIngredients()
        {
            await AddNewIngredientViewModel.GetAllIngredients();

        }

    }
}
