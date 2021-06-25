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

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.RestManager.SearchUsersAsync();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new UserDetailsPage
            {
                BindingContext = e.SelectedItem as UserItem
            });
        }
    }
}
