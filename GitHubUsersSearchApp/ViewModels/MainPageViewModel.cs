using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GitHubUsersSearchApp.Models;
using GitHubUsersSearchApp.Views;
using Xamarin.Forms;

namespace GitHubUsersSearchApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserItem> Users { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand EntryCompletedCommand { get; private set; }
        public ICommand UserSelectedCommand { get; private set; }

        public object SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }
        private object selectedUser;

        public MainPageViewModel()
        {
            EntryCompletedCommand = new Command(async (arg) => await SearchUsersAsync(arg));
            UserSelectedCommand = new Command(async(arg) => await GoToUserDetails(arg));
        }

        public async Task SearchUsersAsync(object arg)
        {
            if (Users != null)
            {
                Users.Clear();
            }
            OnPropertyChanged("Users");

            List<UserItem> users = await App.RestManager.SearchUsersAsync((string)arg);
            Users = new ObservableCollection<UserItem>(users);
            OnPropertyChanged("Users");
        }

        public async Task GoToUserDetails(object arg)
        {
            if (arg != null)
            {
                UserItem user = (UserItem)arg;
                await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?{nameof(UserDetailsPage.UserId)}={user.id}");
                SelectedUser = null;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
