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
                UserItem user = App.RestManager.GetUserItem(userId);
                if (user != null)
                {
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
