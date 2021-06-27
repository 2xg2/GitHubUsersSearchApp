using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubUsersSearchApp.Models;

namespace GitHubUsersSearchApp.Data
{
    public class RestManager
    {
        public static string SearchUsersBaseUrl = "https://api.github.com/search/users";
        
        private IRestService restService;
        private SearchUsersResponse SearchReseponse;

        public RestManager(IRestService service)
        {
            restService = service;
        }

        public async Task<SearchUsersResponse> SearchUsersAsync(string searchText, int page = 1, int perPage = 30)
        {
            SearchReseponse = await restService.SearchUsersAsync(searchText, page, perPage);
            return SearchReseponse;
        }

        public UserItem GetUserItem(string id)
        {
            UserItem user = null;
            if (SearchReseponse != null)
            {
                if (Int32.TryParse(id, out int idInt))
                {
                    user = SearchReseponse.items.Find(x => x.id == idInt);
                }
            }
            return user;
        }

        public static string GetSearchUri(string searchText, int page, int perPage)
        {
            string searchUri = SearchUsersBaseUrl;
            searchUri += "?";
            searchUri += string.Format("q={0}", searchText);
            searchUri += string.Format("&page={0}", page);
            searchUri += string.Format("&per_page={0}", perPage);
            return searchUri;
        }
    }
}
