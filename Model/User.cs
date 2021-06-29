using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace CeseatUserManagement.UserManagementSpace
{
    class User : IUser
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<IObserver> observers = new List<IObserver>();

        public string Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        private string _role = "";
        public string Role
        {
            get => _role;
            set
            {
                if (value != _role)
                {
                    _role = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Role"));
                    }
                    Notify();
                }
            }
        }

        public User(string id, string email, string lastName, string firstName, string role)
        {
            this.Id = id;
            this.Email = email;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.Role = role;
        }

        public void Attach(IObserver observer)
        {
            if (!this.observers.Contains(observer))
            {
                this.observers.Add(observer);
            }
        }

        public void Detach(IObserver observer)
        {
            this.observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in this.observers)
            {
                observer.updateUser(this);
            }
        }
    }
}
