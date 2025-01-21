
using RecipeApp_labb3Db.Presentation.Command;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    internal class TopMenuViewModel : ViewModelBase
    {
        

        public ViewModelBase _selectedView;
        public ViewModelBase SelectedView
        {
            get => _selectedView;
            set
            {
                _selectedView = value;
                OnPropertyChanged();
            }
        }
        public RecipeMenuViewModel RecipeMenuViewModel { get; }
        public AddNewIngredientViewModel AddNewIngredientViewModel { get; }

        public RelayCommand SwapToRecipeViewCommand { get; }
        public RelayCommand SwapToIngredientViewCommand { get; }

        public TopMenuViewModel()
        {
            RecipeMenuViewModel = new RecipeMenuViewModel();
            AddNewIngredientViewModel = new AddNewIngredientViewModel();
            SwapToIngredientViewCommand = new RelayCommand(SwapToIngredientView);
            SwapToRecipeViewCommand = new RelayCommand(SwapToRecipeView);
            
        }

        public void SwapToRecipeView(object? arg)
        {
            SelectedView = RecipeMenuViewModel;
        }

        public void SwapToIngredientView(object? arg)
        {
            
            SelectedView = AddNewIngredientViewModel;
        }
    }
}
