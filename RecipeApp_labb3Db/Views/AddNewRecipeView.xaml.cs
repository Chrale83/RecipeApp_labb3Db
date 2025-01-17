using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace RecipeApp_labb3Db.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AddNewRecipeView.xaml
    /// </summary>
    public partial class AddNewRecipeView : UserControl
    {
        public AddNewRecipeView()
        {
            InitializeComponent();
        }

        private void isnumeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

