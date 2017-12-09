using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatoIndustry.Helper;
using MatoIndustry.Model;
using MatoIndustry.Server;

namespace MatoIndustry.ViewModel
{
    public class RecipeListPageViewModel : ViewModelBase
    {
        private readonly RecipeServer recipeServer = new RecipeServer();
        private int _currentPageIndex = 1;

        public RecipeListPageViewModel()
        {
            this.PropertyChanged += RecipeListPageViewModel_PropertyChanged;
            this.LoadMoreRecipeCommand = new RelayCommand(LoadMoreAction);
            this.PropertyChanged += RecipeListPageViewModel_PropertyChanged1;
        }

        private void RecipeListPageViewModel_PropertyChanged1(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentRecipeDetailItems) && CurrentRecipeDetailItems != null)
            {
                var id = CurrentRecipeDetailItems.MenuId;
                CommonHelper.GoNavigate("RecipeDetailPage", args: new[] { id });
            }
        }

        private async void LoadMoreAction()
        {
            var temp = await recipeServer.GetRecipeListEntityByCid(Id, _currentPageIndex);
            if (temp.Result != null)
            {

                _currentPageIndex = temp.Result.CurPage + 1;

                var source = CommonHelper.ReSeletionValue(temp.Result.List);
                foreach (var item in source)
                {
                    CookListItems.Add(item);
                }
            }
        }

        public RecipeListPageViewModel(CategoryInfo info) : this()
        {
            Id = info.CtgId;
            Name = info.Name;

        }
        private async void RecipeListPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Id))
            {
                var temp = await recipeServer.GetRecipeListEntityByCid(Id, _currentPageIndex);
                if (temp.Result != null)
                {
                    _currentPageIndex = temp.Result.CurPage + 1;
                    var source = CommonHelper.ReSeletionValue(temp.Result.List);
                    CookListItems = new ObservableCollection<RecipeDetailInfo>(source);
                }
            }
        }


        private string _id;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
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

        public RelayCommand LoadMoreRecipeCommand { get; set; }
    }
}
