using System;
using System.Collections.Generic;
using System.Text;

namespace CeseatUserManagement.UserManagementSpace
{
    interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}
