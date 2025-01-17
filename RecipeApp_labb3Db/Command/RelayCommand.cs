using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipeApp_labb3Db.Presentation.Command
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> _exectue;
        private readonly Func<object?, bool>? _canExectue;
        private RelayCommand? showRecipeListCommand;

        public event EventHandler? CanExecuteChanged; //Fungerar som propertyChanged

        public RelayCommand(Action<object> exectue, Func<object?, bool>? canExectue = null) //Den kan vara en referns till en metod som tar ett object in
        {
            ArgumentNullException.ThrowIfNull(exectue);
            _exectue = exectue;
            _canExectue = canExectue;
        }

        public RelayCommand(RelayCommand? showRecipeListCommand)
        {
            this.showRecipeListCommand = showRecipeListCommand;
        }

        public void RaiseCanExectueChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) //Den kör denna först(om jag använder den), sen Execute() Returnerar en bool
        {
            return _canExectue is null ? true : _canExectue(parameter);
        }

        public void Execute(object? parameter) //Den koden som körs när man trycker på knappen, men bara om CanExecute() är true 
        {
            _exectue(parameter);
        }
    }
}
