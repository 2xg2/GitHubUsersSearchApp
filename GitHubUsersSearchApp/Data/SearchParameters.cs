using System;
namespace GitHubUsersSearchApp.Data
{
    public class SearchParameters
    {
        public enum ESortType
        {
            bestmatch,
            followers,
            repositories,
            joined,
        }

        public enum EOrderType
        {
            desc,
            asc,
        }

        public ESortType Sort;
        public EOrderType Order;

        public SearchParameters()
        {

        }

        public SearchParameters(SearchParameters parameters)
        {
            Sort = parameters.Sort;
            Order = parameters.Order;
        }
    }
}
