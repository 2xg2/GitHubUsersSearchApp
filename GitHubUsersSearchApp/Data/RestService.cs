using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubUsersSearchApp.Models;

namespace GitHubUsersSearchApp.Data
{
    public class RestService : IRestService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public RestService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<UserItem>> SearchUsersAsync(string searchText)
        {
            SearchUsersResponse searchResponse = new SearchUsersResponse();
            List<UserItem> users = new List<UserItem>();

            Uri uri = new Uri(string.Format("https://api.github.com/search/users?q={0}", searchText));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    searchResponse = JsonSerializer.Deserialize<SearchUsersResponse>(content, serializerOptions);
                    users = searchResponse.items;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return users;
        }
    }
}
