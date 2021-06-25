using System;
using GitHubUsersSearchApp.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GitHubUsersSearchApp
{
    public partial class App : Application
    {
        public static RestManager RestManager { get; private set; }
        public App()
        {
            InitializeComponent();

            RestManager = new RestManager(new RestService());
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
