using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatoIndustry.Helper;
using MatoIndustry.Model;
using MatoIndustry.Server;

namespace MatoIndustry.ViewModel
{
    public class RecommendPageViewModel : ViewModelBase
    {

        private readonly RecipeServer recipeServer = new RecipeServer();

        public RecommendPageViewModel()
        {
            this.PropertyChanged += RecommendPageViewModel_PropertyChanged;
            InitCookList();
            InitSearchHistory();
            this.RecommendItemClickedCommand = new RelayCommand(RecommendItemClickedAction);
        }

        public void RecommendItemClickedAction()
        {
            if (CurrentRecommendRecipeDetailItems != null)
            {
                var id = CurrentRecommendRecipeDetailItems.MenuId;
                CommonHelper.GoNavigate("RecipeDetailPage", args: new[] { id });

            }
        }

        private void RecommendPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CookListItems))
            {

            }

            else if (e.PropertyName == nameof(CurrentRecipeDetailItems) && CurrentRecipeDetailItems != null)
            {
                var id = CurrentRecipeDetailItems.MenuId;
                CommonHelper.GoNavigate("RecipeDetailPage", args: new[] { id });
            }

            else if (e.PropertyName == nameof(CurrentSearchWordsInfo) && CurrentSearchWordsInfo != null)
            {
                CommonHelper.GoNavigate("SearchPage", Common.NavigationType.SwitchTab, args: new[] { CurrentSearchWordsInfo.Words });
            }
        }

        /// <summary>
        /// 初始化推荐菜
        /// </summary>
        public async void InitCookList()
        {

            System.DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            var timeStamp = (int)(DateTime.Now - startTime).TotalDays;
            var isOdd = timeStamp % 2 == 0;
            var todayIndex = timeStamp % 1950 + (isOdd ? 40 : 10);
            var thisWeekIndex = (timeStamp / 5) % 1950 + (isOdd ? 40 : 10);
            var todaytemp = await recipeServer.GetRecipeListEntity(todayIndex);
            var thisWeektemp = await recipeServer.GetRecipeListEntity(thisWeekIndex);
            if (todaytemp.Result != null)
            {
                CookListItems = CommonHelper.ReSeletionValue(thisWeektemp.Result.List);

            }
            if (thisWeektemp.Result != null)
            {
                if (todaytemp.Result != null)
                    RecommendItems = CommonHelper.ReSeletionValue(todaytemp.Result.List).Take(5).ToList();
            }

        }

        /// <summary>
        /// 初始化推荐词
        /// </summary>
        public async void InitSearchHistory()
        {
            this.HotSearchWords = await SearchHistoryServer.Current.GetHotSearchList();
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

        private RecipeDetailInfo _currentRecommendRecipeDetailItems;

        public RecipeDetailInfo CurrentRecommendRecipeDetailItems
        {
            get
            {

                return _currentRecommendRecipeDetailItems;
            }
            set
            {

                _currentRecommendRecipeDetailItems = value;
                RaisePropertyChanged();
            }
        }

        private List<RecipeDetailInfo> _cookListItems;

        public List<RecipeDetailInfo> CookListItems
        {
            get
            {
                if (_cookListItems == null)
                {
                    _cookListItems = new List<RecipeDetailInfo>();
                }
                return _cookListItems;
            }
            set
            {

                _cookListItems = value;
                RaisePropertyChanged();
            }
        }
        private List<RecipeDetailInfo> _recommendItems;

        public List<RecipeDetailInfo> RecommendItems
        {
            get
            {
                if (_recommendItems == null)
                {
                    _recommendItems = new List<RecipeDetailInfo>();
                }
                return _recommendItems;
            }
            set
            {

                _recommendItems = value;
                RaisePropertyChanged();
            }
        }
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

        public RelayCommand RecommendItemClickedCommand { get; set; }

    }
}