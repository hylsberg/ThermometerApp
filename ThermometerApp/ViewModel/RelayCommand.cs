using System;
using System.Windows.Input;

namespace ThermometerApp
{
    /// <summary>
    /// RelayCommand er en Klasse, der implementerer ICommand og bruges til at oprette Commands (simpelt)
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Deligates
        private Action<object> execute;
        private Predicate<object> canExecute;
        #endregion

        #region Construktors
        //construktor tillader at man kalder med kun Execute deligate
        //Dvs. laver en Command der altid kan Execute
        public RelayCommand(Action<object> execute):this(execute,null) { }

        //laver en Command
        public RelayCommand(Action<object> execute, Predicate<object> canExecute )
        {
            if (execute==null)
            {
                throw new ArgumentNullException();
            }
           this.execute = execute;     
           this.canExecute = canExecute;     
        }
        #endregion

        #region ICommand Implementation

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //kalder canExecute (returenerer true hvis den er null)
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);     
        }     
        //kaldet execute
        public void Execute(object parameter)
        {     
            this.execute(parameter);     
        }
        #endregion
    }
}
