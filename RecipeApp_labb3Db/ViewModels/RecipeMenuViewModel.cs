﻿using RecipeApp_labb3Db.Presentation.Command;

namespace RecipeApp_labb3Db.Presentation.ViewModels
{
    internal class RecipeMenuViewModel : ViewModelBase
    {
        public AddNewRecipeViewModel AddNewRecipeViewModel { get; }
        public RecipeListViewModel RecipeListViewModel { get; }
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
            RecipeListViewModel = new RecipeListViewModel();
            SelectedRecipeView = AddNewRecipeViewModel;
            ShowCreateRecipeCommand = new RelayCommand(ShowCreateRecipeView);
            ShowRecipeListCommand = new RelayCommand(ShowRecipeListView);

        }

        private async void ShowRecipeListView(object obj)
        {
            await RecipeListViewModel.GetAllRecipes();
            SelectedRecipeView = RecipeListViewModel;
        }

        private async void ShowCreateRecipeView(object obj)
        {
            await AddNewRecipeViewModel.GetAllRecipes();
            SelectedRecipeView = AddNewRecipeViewModel;
        }

        public RelayCommand ShowRecipeListCommand { get; }
        public RelayCommand ShowCreateRecipeCommand { get; }
    }
}
