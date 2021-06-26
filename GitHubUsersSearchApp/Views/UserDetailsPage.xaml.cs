using System;
using System.Diagnostics;
using GitHubUsersSearchApp.Models;
using Xamarin.Forms;

namespace GitHubUsersSearchApp.Views
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public partial class UserDetailsPage : ContentPage
    {
        public string UserId
        {
            set
            {
                LoadUser(value);
            }
        }
        public UserDetailsPage()
        {
            InitializeComponent();
        }

        void LoadUser(string userId)
        {
            try
            {
                if (Int32.TryParse(userId, out int userIdInt))
                {
                    UserItem user = App.RestManager.LastSearchedUserItems.Find(x => x.id == userIdInt);
                    BindingContext = user;
                }
            }
            catch(Exception)
            {
                Debug.WriteLine("Failed to load user");
            }
        }
    }
}
