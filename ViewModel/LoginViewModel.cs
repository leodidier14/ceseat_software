using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace CeseatUserManagement.LoginSpace
{
    class LoginViewModel
    {
        private LoginService LoginService { get; set; }

        public LoginViewModel()
        {
            this.LoginService = new LoginService();
        }

        public Boolean login(string identifiant, string password)
        {
            this.LoginService.login(identifiant, password);
            return true;
        }
    }
}
