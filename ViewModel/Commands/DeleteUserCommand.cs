using System;
using System.Windows.Input;

namespace CeseatUserManagement.UserManagementSpace.Commands
{
    class DeleteUserCommand : ICommand
    {
        public UserManagementViewModel ViewModel { get; set; }

        public DeleteUserCommand(UserManagementViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            return false;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.ViewModel.deleteUser(parameter as User);
        }
    }
}
