using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GitHubUsersSearchApp.Models;

namespace GitHubUsersSearchApp.Data
{
    public class RestManager
    {
        public static string SearchUsersBaseUrl = "https://api.github.com/search/users";
        public SearchParameters SearchParameters { get; private set; }

        private IRestService restService;
        private SearchUsersResponse SearchReseponse;

        public RestManager(IRestService service)
        {
            restService = service;
            SearchParameters = new SearchParameters();
        }

        public void SetSearchParameters(SearchParameters searchParameters)
        {
            SearchParameters = searchParameters;
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

        public string GetSearchUri(string searchText, int page, int perPage)
        {
            string searchUri = SearchUsersBaseUrl;
            searchUri += "?";
            searchUri += string.Format("q={0}", searchText);

            if(SearchParameters != null)
            {
                if(SearchParameters.Type != SearchParameters.ETypeType.all)
                {
                    searchUri += string.Format("+type:{0}", SearchParameters.Type);
                }
                if(SearchParameters.InName)
                {
                    searchUri += string.Format("+in:name", string.Empty);
                }
                if(SearchParameters.InLogin)
                {
                    searchUri += string.Format("+in:login", string.Empty);
                }
                if(SearchParameters.InEmail)
                {
                    searchUri += string.Format("+in:email", string.Empty);
                }

                if(SearchParameters.Sort != SearchParameters.ESortType.bestmatch)
                {
                    searchUri += string.Format("&sort={0}", SearchParameters.Sort);
                    searchUri += string.Format("&order={0}", SearchParameters.Order);
                }
            }

            searchUri += string.Format("&page={0}", page);
            searchUri += string.Format("&per_page={0}", perPage);

            Debug.WriteLine("searchUri = " + searchUri);
            return searchUri;
        }
    }
}
