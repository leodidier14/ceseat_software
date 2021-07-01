using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace CeseatUserManagement.UserManagementSpace
{
    class User : IUser, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<IObserver> observers = new List<IObserver>();

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        private string _role = "";

        [JsonProperty("userType")]
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

        private Boolean _isSuspended = false;
        [JsonProperty("isSuspended")]
        public Boolean IsSuspended
        {
            get => _isSuspended;
            set
            {
                if (value != _isSuspended)
                {
                    _isSuspended = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSuspended"));
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
