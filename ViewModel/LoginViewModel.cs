using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CeseatUserManagement.LoginSpace
{
    class LoginViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private LoginService LoginService { get; set; }

        private string _loginResponse = "Test";
        public string LoginResponse
        {
            get => _loginResponse;

            set
            {
                if (value != _loginResponse)
                {
                    _loginResponse = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("LoginResponse"));
                    }
                }
            }
        }

        public LoginViewModel()
        {
            this.LoginService = new LoginService();
        }

        public async Task<Boolean> login(string identifiant, string password)
        {
            try
            {
                await this.LoginService.login(identifiant, password);
            }
            catch
            {

            }
            if (!String.IsNullOrEmpty(AccesToken.GetInstance().AccesTokenValue))
            {
                return true;
            }
            return false;
        }
    }
}
