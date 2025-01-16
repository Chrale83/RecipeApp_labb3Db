namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    internal class RecipeMenuViewModel : ViewModelBase
    {
        public AddNewRecipeViewModel AddNewRecipeViewModel { get; }
        private ViewModelBase _selectedRecipeView;

        public ViewModelBase SelectedRecipeView
        {
            get => _selectedRecipeView;
            set
            {
                _selectedRecipeView = value;
                OnPropertyChanged();
            }
        }

        public RecipeMenuViewModel()
        {
            AddNewRecipeViewModel = new AddNewRecipeViewModel();
            SelectedRecipeView = AddNewRecipeViewModel;
        }


    }
}
