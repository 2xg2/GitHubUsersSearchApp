using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GitHubUsersSearchApp.Models;
using GitHubUsersSearchApp.Views;
using Xamarin.Forms;

namespace GitHubUsersSearchApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SearchStartCommand { get; private set; }
        public ICommand SearchPreviousPageCommand { get; private set; }
        public ICommand SearchNextPageCommand { get; private set; }
        public ICommand UserSelectedCommand { get; private set; }

        public string SearchEntryText
        {
            get
            {
                return searchEntryText;
            }
            set
            {
                if (searchEntryText != value)
                {
                    searchEntryText = value;
                    OnPropertyChanged("SearchEntryText");
                }
            }
        }
        private string searchEntryText;

        public bool ResultsVisible
        {
            get
            {
                return resultsVisible;
            }
            set
            {
                if (resultsVisible != value)
                {
                    resultsVisible = value;
                    OnPropertyChanged("ResultsVisible");
                }
            }
        }
        private bool resultsVisible;

        public string ResultsCountText {
            get
            {
                return resultsCountText;
            }
            set
            {
                if(resultsCountText != value)
                {
                    resultsCountText = value;
                    OnPropertyChanged("ResultsCountText");
                }
            }
        }
        private string resultsCountText;

        public string ResultsPageText
        {
            get
            {
                return resultsPageText;
            }
            set
            {
                if (resultsPageText != value)
                {
                    resultsPageText = value;
                    OnPropertyChanged("ResultsPageText");
                }
            }
        }
        private string resultsPageText;

        public bool PreviousPageButtonVisible
        {
            get
            {
                return previousPageButtonVisible;
            }
            set
            {
                if (previousPageButtonVisible != value)
                {
                    previousPageButtonVisible = value;
                    OnPropertyChanged("PreviousPageButtonVisible");
                }
            }
        }
        private bool previousPageButtonVisible;

        public bool NextPageButtonVisible
        {
            get
            {
                return nextPageButtonVisible;
            }
            set
            {
                if (nextPageButtonVisible != value)
                {
                    nextPageButtonVisible = value;
                    OnPropertyChanged("NextPageButtonVisible");
                }
            }
        }
        private bool nextPageButtonVisible;

        public bool ResultsPageVisible
        {
            get
            {
                return resultsPageVisible;
            }
            set
            {
                if (resultsPageVisible != value)
                {
                    resultsPageVisible = value;
                    OnPropertyChanged("ResultsPageVisible");
                }
            }
        }
        private bool resultsPageVisible;

        public bool ResultsListVisible
        {
            get
            {
                return resultsListVisible;
            }
            set
            {
                if (resultsListVisible != value)
                {
                    resultsListVisible = value;
                    OnPropertyChanged("ResultsListVisible");
                }
            }
        }
        private bool resultsListVisible;

        public bool ResultsNoItemsVisible
        {
            get
            {
                return resultsNoItemsVisible;
            }
            set
            {
                if (resultsNoItemsVisible != value)
                {
                    resultsNoItemsVisible = value;
                    OnPropertyChanged("ResultsNoItemsVisible");
                }
            }
        }
        private bool resultsNoItemsVisible;

        public object SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }
        private object selectedUser;

        public ObservableCollection<UserItem> Users { get; private set; }

        private int perPageCount = 30;
        private int currentPageIndex;
        private int pagesCount;

        public MainPageViewModel()
        {
            SearchStartCommand = new Command(async (arg) => await SearchUsersStartAsync(arg));
            SearchPreviousPageCommand = new Command(async (arg) => await SearchPreviousPageAsync(arg));
            SearchNextPageCommand = new Command(async (arg) => await SearchNextPageAsync(arg));
            UserSelectedCommand = new Command(async(arg) => await GoToUserDetails(arg));
        }

        public async Task SearchUsersStartAsync(object arg)
        {
            currentPageIndex = 0;
            pagesCount = 0;

            await SearchUsersAsync(SearchEntryText, currentPageIndex, perPageCount);
        }

        public async Task SearchPreviousPageAsync(object arg)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                await SearchUsersAsync(SearchEntryText, currentPageIndex, perPageCount);
            }
        }

        public async Task SearchNextPageAsync(object arg)
        {
            if(currentPageIndex < pagesCount - 1)
            {
                currentPageIndex++;
                await SearchUsersAsync(SearchEntryText, currentPageIndex, perPageCount);
            }
        }

        public async Task GoToUserDetails(object arg)
        {
            if (arg != null)
            {
                UserItem user = (UserItem)arg;
                await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?{nameof(UserDetailsPage.UserId)}={user.id}");
                SelectedUser = null;
            }
        }

        private async Task SearchUsersAsync(string searchText, int page, int perPage)
        {
            if (Users != null)
            {
                Users.Clear();
            }
            OnPropertyChanged("Users");
            ResultsVisible = false;

            SearchUsersResponse response = await App.RestManager.SearchUsersAsync(searchText, page + 1, perPage);
            ProcessSearchUsersResponse(response);
        }

        private void ProcessSearchUsersResponse(SearchUsersResponse response)
        {
            if (response != null)
            {
                bool noItems = false;
                if (response.total_count > 0)
                {
                    if (perPageCount != 0)
                    {
                        pagesCount = (int)Math.Ceiling((float)response.total_count / (float)perPageCount);
                    }
                }
                else
                {
                    noItems = true;
                }

                if (noItems && pagesCount > 0)
                {
                    //error occured
                    //one of the previous requests for this search text was successful, don't update ResultsCountText with 0
                }
                else
                {
                    ResultsCountText = response.total_count.ToString();
                }

                ResultsPageVisible = (pagesCount > 0);
                if (ResultsPageVisible)
                {
                    ResultsPageText = string.Format("Page: {0}/{1}", currentPageIndex + 1, pagesCount);
                    UpdatePageNavigationButtonsVisibility();
                }

                ResultsNoItemsVisible = noItems;
                ResultsListVisible = !noItems;
                if (ResultsListVisible)
                {
                    UpdateUsersListUI(response);
                }

                ResultsVisible = true;
            }
        }

        private void UpdateUsersListUI(SearchUsersResponse response)
        {
            if (response != null && response.items != null)
            {
                List<UserItem> users = response.items;
                Users = new ObservableCollection<UserItem>(users);
                OnPropertyChanged("Users");
            }
        }

        private void UpdatePageNavigationButtonsVisibility()
        {
            PreviousPageButtonVisible = (currentPageIndex > 0);
            NextPageButtonVisible = (currentPageIndex < pagesCount - 1);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
