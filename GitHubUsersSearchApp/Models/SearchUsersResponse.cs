using System.Collections.Generic;

namespace GitHubUsersSearchApp.Models
{
    public class SearchUsersResponse
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<UserItem> items { get; set; }
    }
}
