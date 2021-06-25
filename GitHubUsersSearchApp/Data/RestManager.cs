using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubUsersSearchApp.Models;

namespace GitHubUsersSearchApp.Data
{
    public class RestManager
    {
        IRestService restService;

        public RestManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<UserItem>> SearchUsersAsync(string searchText)
        {
            return restService.SearchUsersAsync(searchText);
        }
    }
}
