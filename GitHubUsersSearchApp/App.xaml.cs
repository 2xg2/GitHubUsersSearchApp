using GitHubUsersSearchApp.Data;
using Xamarin.Forms;

namespace GitHubUsersSearchApp
{
    public partial class App : Application
    {
        public static RestManager RestManager { get; private set; }
        public App()
        {
            InitializeComponent();

            RestManager = new RestManager(new RestService());
            MainPage = new AppShell();
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
