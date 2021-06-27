using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GitHubUsersSearchApp.Data;
using Xamarin.Forms;

namespace GitHubUsersSearchApp.ViewModels
{
    public class SearchParametersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SortSelectedIndexChangedCommand { get; private set; }
        public ICommand OrderSelectedIndexChangedCommand { get; private set; }
        public ICommand TypeSelectedIndexChangedCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public ObservableCollection<string> SortTypes { get; private set; }
        public int SortSelectedIndex
        {
            get
            {
                return sortSelectedIndex;
            }
            set
            {
                if (sortSelectedIndex != value)
                {
                    sortSelectedIndex = value;
                    OnPropertyChanged("SortSelectedIndex");
                }
            }
        }
        private int sortSelectedIndex;

        public ObservableCollection<string> OrderTypes { get; private set; }
        public int OrderSelectedIndex
        {
            get
            {
                return orderSelectedIndex;
            }
            set
            {
                if (orderSelectedIndex != value)
                {
                    orderSelectedIndex = value;
                    OnPropertyChanged("OrderSelectedIndex");
                }
            }
        }
        private int orderSelectedIndex;

        public ObservableCollection<string> TypeTypes { get; private set; }
        public int TypeSelectedIndex
        {
            get
            {
                return typeSelectedIndex;
            }
            set
            {
                if (typeSelectedIndex != value)
                {
                    typeSelectedIndex = value;
                    OnPropertyChanged("TypeSelectedIndex");
                }
            }
        }
        private int typeSelectedIndex;

        private SearchParameters currentSearchParameters;

        public SearchParametersViewModel()
        {
            SortSelectedIndexChangedCommand = new Command((arg) => SetSortType(arg));
            OrderSelectedIndexChangedCommand = new Command((arg) => SetOrderType(arg));
            TypeSelectedIndexChangedCommand = new Command((arg) => SetTypeType(arg));
            SaveCommand = new Command(async(arg) => await Save(arg));

            List<string> sortTypesList = Enum.GetNames(typeof(SearchParameters.ESortType)).ToList();
            SortTypes = new ObservableCollection<string>(sortTypesList);
            OnPropertyChanged("SortTypes");

            List<string> orderTypesList = Enum.GetNames(typeof(SearchParameters.EOrderType)).ToList();
            OrderTypes = new ObservableCollection<string>(orderTypesList);
            OnPropertyChanged("OrderTypes");

            List<string> typeTypesList = Enum.GetNames(typeof(SearchParameters.ETypeType)).ToList();
            TypeTypes = new ObservableCollection<string>(typeTypesList);
            OnPropertyChanged("TypeTypes");

            currentSearchParameters = new SearchParameters(App.RestManager.SearchParameters);
            SortSelectedIndex = (int)currentSearchParameters.Sort;
            OrderSelectedIndex = (int)currentSearchParameters.Order;
            TypeSelectedIndex = (int)currentSearchParameters.Type;
        }

        private void SetSortType(object arg)
        {
            currentSearchParameters.Sort = (SearchParameters.ESortType)SortSelectedIndex;
        }

        private void SetOrderType(object arg)
        {
            currentSearchParameters.Order = (SearchParameters.EOrderType)OrderSelectedIndex;
        }

        private void SetTypeType(object arg)
        {
            currentSearchParameters.Type = (SearchParameters.ETypeType)TypeSelectedIndex;
        }

        private async Task Save(object arg)
        {
            App.RestManager?.SetSearchParameters(currentSearchParameters);
            await Shell.Current.GoToAsync("..");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
