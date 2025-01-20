using System.Windows;

namespace RecipeApp_labb3Db.Presentation.Services
{
    public class DialogService
    {
        public static bool ShowConfirmationDialog(string message, string title)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);
            return result == MessageBoxResult.Yes;
        }

        public void ShowErrorDialog(string message, string title)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
