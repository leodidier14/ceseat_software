using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace CeseatUserManagement.UserManagementSpace.Commands
{
    class DeleteUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
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

        public void Execute(object parameter)
        {
            this.ViewModel.deleteUser(parameter as IUser);
        }
    }
}
