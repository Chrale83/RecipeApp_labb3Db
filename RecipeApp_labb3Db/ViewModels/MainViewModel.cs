using RecipeDbAccess.DataAccess;

using RecipeDbAccess.Models;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public TopMenuViewModel TopMenuViewModel { get; }
        public RecipeMenuViewModel RecipeMenuViewModel { get; }

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


        public MainViewModel()
        {
            TopMenuViewModel = new TopMenuViewModel();
            RecipeMenuViewModel = new RecipeMenuViewModel();
            SelectedView = RecipeMenuViewModel;
            IngredientDataAccess = new IngredientDataAccess();
            

        }

        public IngredientDataAccess IngredientDataAccess;

        
    }
}
