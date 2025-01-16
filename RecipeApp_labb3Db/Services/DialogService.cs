using System.Windows;

namespace RecipeApp_labb3Db.Presentation.Services
{
    public class DialogService : IDialogService
    {
        public bool ShowConfirmationDialog(string message, string title)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
    }
}
