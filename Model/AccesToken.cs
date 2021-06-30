using System;
using System.Collections.Generic;
using System.Text;

namespace CeseatUserManagement
{
    class AccesToken
    {
        public string AccesTokenValue {get; set;}

        private AccesToken() { }

        private static AccesToken _instance;

        public static AccesToken GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccesToken();
            }
            return _instance;
        }
    }
}
