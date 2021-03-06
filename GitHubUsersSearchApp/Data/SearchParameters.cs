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

        public enum ETypeType
        {
            all,
            user,
            org,
        }

        public ESortType Sort;
        public EOrderType Order;
        public ETypeType Type;
        public bool InName;
        public bool InLogin;
        public bool InEmail;

        public SearchParameters()
        {

        }

        public SearchParameters(SearchParameters parameters)
        {
            Sort = parameters.Sort;
            Order = parameters.Order;
            Type = parameters.Type;
            InName = parameters.InName;
            InLogin = parameters.InLogin;
            InEmail = parameters.InEmail;
        }
    }
}
