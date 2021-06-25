using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubUsersSearchApp.Models;
using Xamarin.Forms;

namespace GitHubUsersSearchApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new UserDetailsPage
                {
                    BindingContext = e.SelectedItem as UserItem
                });

                listView.SelectedItem = null;
            }
        }

        async void EntryCompleted(object sender, EventArgs e)
        {
            listView.ItemsSource = await App.RestManager.SearchUsersAsync(((Entry)sender).Text);
        }
    }
}
