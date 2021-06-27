using GitHubUsersSearchApp.Views;
using Xamarin.Forms;

namespace GitHubUsersSearchApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
            Routing.RegisterRoute(nameof(SearchParametersPage), typeof(SearchParametersPage));
        }
    }
}
