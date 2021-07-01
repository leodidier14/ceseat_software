using CeseatUserManagement.UserManagementSpace.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Data;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;

namespace CeseatUserManagement.UserManagementSpace
{
    class UserManagementViewModel : IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private UserService userService;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<string> FilterRoles { get; set; }
        public ObservableCollection<string> UserRoles { get; set; }
        private CollectionViewSource CVS { get; set; }

        private string _selectedFilterRole = "Tous";
        public string SelectedFilterRole
        {
            get => _selectedFilterRole;

            set
            {
                if (value != _selectedFilterRole)
                {
                    _selectedFilterRole = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedFilterRole"));
                    }
                    ApplyFilter(!string.IsNullOrEmpty(_selectedFilterRole) ? FilterField.Role : FilterField.None);
                }
            }
        }

        private string _searchingName = "";
        public string SearchingName
        {
            get => _searchingName;

            set
            {
                if (value != _searchingName)
                {
                    _searchingName = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SearchingName"));
                    }
                    ApplyFilter(FilterField.Name);
                }
            }
        }

        private Boolean _canRemoveRoleFilter = false;
        public Boolean CanRemoveRoleFilter
        {
            get => _canRemoveRoleFilter;

            set
            {
                if (value != _canRemoveRoleFilter)
                {
                    _canRemoveRoleFilter = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CanRemoveRoleFilter"));
                    }
                }
            }
        }

        private Boolean _canRemoveNameFilter = false;
        public Boolean CanRemoveNameFilter
        {
            get => _canRemoveNameFilter;

            set
            {
                if (value != _canRemoveNameFilter)
                {
                    _canRemoveNameFilter = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CanRemoveNameFilter"));
                    }
                }
            }
        }

        private enum FilterField
        {
            Role,
            Name,
            None,
        }

        public ICommand RefreshDataCommand
        {
            get;
            private set;
        }

        public DeleteUserCommand DeleteUserCommand
        {
            get;
            private set;
        }

        public UserManagementViewModel()
        {
            this.Users = new ObservableCollection<User>();
            this.FilterRoles = new ObservableCollection<string>();
            this.UserRoles = new ObservableCollection<string>();

            this.initCommands();
            this.userService = new UserService();
            this.loadData();
            
            Messenger.Default.Register<CollectionViewSource>(this, registerCVS);
        }

        private void registerCVS(CollectionViewSource source)
        {
            this.CVS = source;
        }

        private void initCommands()
        {
            this.RefreshDataCommand = new RelayCommand(this.loadData, null);
            this.DeleteUserCommand = new DeleteUserCommand(this);
        }

        private async void loadData()
        {
            List<User> _users = new List<User>();
            await Task.Run(() =>
            {
                _users = this.userService.getUsers().Result;
            });


            var _roles = from u in _users select u.Role;

            this.UserRoles.Clear();
            foreach (string role in _roles.Distinct())
            {
                this.UserRoles.Add(role);
            }

            var _roleList = _roles.ToList();
            _roleList.Add("Tous");
            
            this.FilterRoles.Clear();
            foreach (string filter in _roleList.Distinct())
            {
                this.FilterRoles.Add(filter);
            }

            this.SelectedFilterRole = "Tous";

            foreach (User user in _users)
            {
                user.Attach(this);
            }

            this.Users.Clear();
            foreach (User user in _users)
            {
                this.Users.Add(user);
            }
        }

        private void ApplyFilter(FilterField field)
        {
            switch (field)
            {
                case FilterField.Role:
                    addRoleFilter();
                    break;
                case FilterField.Name:
                    addNameFilter();
                    break;
                default:
                    break;
            }
        }

        public void addRoleFilter()
        {
            if (CanRemoveRoleFilter)
            {
                CVS.Filter -= new FilterEventHandler(FilterByRole);
                CVS.Filter += new FilterEventHandler(FilterByRole);
            }
            else
            {
                CVS.Filter += new FilterEventHandler(FilterByRole);
                CanRemoveRoleFilter = true;
            }
        }

        public void addNameFilter()
        {
            if (CanRemoveNameFilter)
            {
                CVS.Filter -= new FilterEventHandler(FilterByName);
                CVS.Filter += new FilterEventHandler(FilterByName);
            }
            else
            {
                CVS.Filter += new FilterEventHandler(FilterByName);
                CanRemoveNameFilter = true;
            }
        }

        private void FilterByRole(object sender, FilterEventArgs e)
        {
            // see Notes on Filter Methods:
            var src = e.Item as User;
            if (src == null)
                e.Accepted = false;
            else if (string.Compare(SelectedFilterRole, src.Role) != 0 && !string.Equals(SelectedFilterRole, "Tous"))
                e.Accepted = false;
        }

        private void FilterByName(object sender, FilterEventArgs e)
        {
            // see Notes on Filter Methods:
            var src = e.Item as User;
            if (src == null)
                e.Accepted = false;
            else if (!src.LastName.ToUpper().Contains(SearchingName.ToUpper()))
                e.Accepted = false;
        }

        public async void deleteUser(User user)
        {
            if (user == null)
                return;

            await this.userService.deleteUser(user);

            this.loadData();

            //this.Users.Remove(user);
        }

        public void updateUser(User user)
        {
            if (user != null)
                this.userService.updateUser(user);
        }
    }
}
