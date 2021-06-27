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

        public async Task<SearchUsersResponse> SearchUsersAsync(string searchText, int page, int perPage)
        {
            SearchUsersResponse searchResponse = new SearchUsersResponse();
            
            Uri uri = new Uri(RestManager.GetSearchUri(searchText, page, perPage));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    searchResponse = JsonSerializer.Deserialize<SearchUsersResponse>(content, serializerOptions);

                    Debug.WriteLine("response content = " + content);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return searchResponse;
        }
    }
}
