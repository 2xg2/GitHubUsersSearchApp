using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubUsersSearchApp.Models;

namespace GitHubUsersSearchApp.Data
{
    public class RestManager
    {
        public static string SearchUsersUrl = "https://api.github.com/search/users?q={0}";
        public List<UserItem> LastSearchedUserItems;

        private IRestService restService;

        public RestManager(IRestService service)
        {
            restService = service;
        }

        public async Task<List<UserItem>> SearchUsersAsync(string searchText)
        {
            LastSearchedUserItems = await restService.SearchUsersAsync(searchText);
            return LastSearchedUserItems;
        }
    }
}
