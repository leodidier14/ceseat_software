using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CeseatUserManagement.LoginSpace
{
    class LoginViewModel
    {
        private LoginService LoginService { get; set; }

        public LoginViewModel()
        {
            this.LoginService = new LoginService();
        }

        public async Task<Boolean> Login(string email, string password)
        {
            if (!String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(password))
            {
                try
                {
                    await this.LoginService.login(email, password);
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
            return false;
        }
    }
}
