using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatoIndustry.Helper;
using MatoIndustry.Model;
using MatoIndustry.Server;

namespace MatoIndustry.ViewModel
{
    public class SearchPageViewModel : ViewModelBase
    {
        private readonly RecipeServer recipeServer = new RecipeServer();
        private int _currentPageIndex = 1;

        public SearchPageViewModel()
        {
            this.SearchTxt = string.Empty;
            InitSearchHistory();
            this.PropertyChanged += SearchPageViewModel_PropertyChanged;
            this.SearchCommand = new RelayCommand(SearchAction);
            this.QuickSearchCommand = new RelayCommand<string>(QuickSearchAction);
            this.LoadMoreRecipeCommand = new RelayCommand(LoadMoreAction);
            this.CleanHistoryCommand = new RelayCommand(CleanHistoryAction);
            this.CleanSearchTextCommand = new RelayCommand(CleanSearchTextAction);

        }

        private void CleanSearchTextAction()
        {
            this.SearchTxt = string.Empty;
        }

        private async void CleanHistoryAction()
        {
            SearchHistoryList.Clear();
            await SearchHistoryServer.Current.SaveHistoryList(this.SearchHistoryList.ToList());
            RaisePropertyChanged(nameof(SearchHistoryListForShow));

        }

        private async void LoadMoreAction()
        {
            var temp = await recipeServer.GetRecipeListEntityByName(this.SearchTxt, _currentPageIndex);
            if (temp.Result != null)
            {

                _currentPageIndex = temp.Result.CurPage + 1;

                var source =CommonHelper.ReSeletionValue(temp.Result.List);
                foreach (var item in source)
                {
                    CookListItems.Add(item);
                }
            }
        }

        private void SearchPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchHistoryList) && SearchHistoryList != null)
            {
                RaisePropertyChanged(nameof(SearchHistoryListForShow));
            }
            else if (e.PropertyName == nameof(CurrentRecipeDetailItems) && CurrentRecipeDetailItems != null)
            {
                var id = CurrentRecipeDetailItems.MenuId;
                CommonHelper.GoNavigate("RecipeDetailPage", args: new[] { id });
            }
            else if (e.PropertyName == nameof(CurrentSearchWordsInfo) && CurrentSearchWordsInfo != null)
            {
                this.SearchTxt = CurrentSearchWordsInfo.Words;
                SearchAction();

            }
            else if (e.PropertyName == nameof(CookListItems))
            {
                RaisePropertyChanged(nameof(HasResult));
            }
        }

        private async void QuickSearchAction(string obj)
        {
            var searchResultNum = await InitCookList(obj);
            this.SearchResultCount = searchResultNum;

        }

        private async void SearchAction()
        {
            this.CookListItems.Clear();
            var searchResultNum = await InitCookList(this.SearchTxt);
            this.SearchResultCount = searchResultNum;
            if (SearchResultCount > 0)
            {

                if (SearchHistoryList.Count >= 8)
                {
                    SearchHistoryList.Dequeue();
                }
                if (!SearchHistoryList.Any(c => c.Words == this.SearchTxt))
                {
                    SearchHistoryList.Enqueue(new SearchWordsInfo(this.SearchTxt));
                   
                }
                await SearchHistoryServer.Current.SaveHistoryList(this.SearchHistoryList.ToList());
                RaisePropertyChanged(nameof(SearchHistoryListForShow));

            }
        }

        /// <summary>
        /// 初始化搜索历史
        /// </summary>
        public async void InitSearchHistory()
        {
            this.SearchHistoryList = new Queue<SearchWordsInfo>(await SearchHistoryServer.Current.GetHistoryList());
            this.HotSearchWords = await SearchHistoryServer.Current.GetHotSearchList();
        }

        private async Task<int> InitCookList(string name)
        {
            var result = 0;
            _currentPageIndex = 1;
            var temp = await recipeServer.GetRecipeListEntityByName(name, _currentPageIndex);
            if (temp.Result != null)
            {
                _currentPageIndex = temp.Result.CurPage + 1;

                var source = CommonHelper.ReSeletionValue(temp.Result.List);
                result = source.Count;
                CookListItems = new ObservableCollection<RecipeDetailInfo>(source);
            }
            else
            {
                result = -1;
            }
            return result;
        }

        private string _searchTxt;

        public string SearchTxt
        {
            get { return _searchTxt; }
            set
            {
                _searchTxt = value;
                RaisePropertyChanged();
            }
        }

        private int _searchResultCount;

        public int SearchResultCount
        {
            get { return _searchResultCount; }
            set
            {
                _searchResultCount = value;
                RaisePropertyChanged();
            }
        }


        private Queue<SearchWordsInfo> _searchHistoryList;

        public Queue<SearchWordsInfo> SearchHistoryList
        {
            get
            {
                if (_searchHistoryList == null)
                {
                    _searchHistoryList = new Queue<SearchWordsInfo>();
                }

                return _searchHistoryList;
            }
            set
            {
                _searchHistoryList = value;
                RaisePropertyChanged();
            }
        }


        public List<SearchWordsInfo> SearchHistoryListForShow => SearchHistoryList.ToList();

        private List<SearchWordsInfo> _hotSearchWords;

        public List<SearchWordsInfo> HotSearchWords
        {
            get
            {
                if (_hotSearchWords == null)
                {
                    _hotSearchWords = new List<SearchWordsInfo>();
                }
                return _hotSearchWords;
            }
            set
            {
                _hotSearchWords = value;
                RaisePropertyChanged();
            }
        }

        public bool HasResult => CookListItems.Count > 0;


        private SearchWordsInfo _currentSearchWordsInfo;

        public SearchWordsInfo CurrentSearchWordsInfo
        {
            get { return _currentSearchWordsInfo; }
            set
            {
                _currentSearchWordsInfo = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<RecipeDetailInfo> _cookListItems;

        public ObservableCollection<RecipeDetailInfo> CookListItems
        {
            get
            {
                if (_cookListItems == null)
                {
                    _cookListItems = new ObservableCollection<RecipeDetailInfo>();
                }
                return _cookListItems;
            }
            set
            {

                _cookListItems = value;
                RaisePropertyChanged();
            }
        }

        private RecipeDetailInfo _currentRecipeDetailItems;

        public RecipeDetailInfo CurrentRecipeDetailItems
        {
            get
            {

                return _currentRecipeDetailItems;
            }
            set
            {

                _currentRecipeDetailItems = value;
                RaisePropertyChanged();
            }
        }



        public RelayCommand SearchCommand { get; set; }
        public RelayCommand<string> QuickSearchCommand { get; set; }
        public RelayCommand LoadMoreRecipeCommand { get; set; }
        public RelayCommand CleanHistoryCommand { get; set; }
        public RelayCommand CleanSearchTextCommand { get; set; }


    }
}
