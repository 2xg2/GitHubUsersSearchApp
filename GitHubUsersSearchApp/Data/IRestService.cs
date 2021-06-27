using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubUsersSearchApp.Models;

namespace GitHubUsersSearchApp.Data
{
    public interface IRestService
    {
        Task<SearchUsersResponse> SearchUsersAsync(string searchText, int page, int perPage);
    }
}
