using System;
using System.Collections.Generic;
using System.Text;

namespace CeseatUserManagement.UserManagementSpace
{
    interface IObserver
    {
        void updateUser(IUser user);
    }
}
