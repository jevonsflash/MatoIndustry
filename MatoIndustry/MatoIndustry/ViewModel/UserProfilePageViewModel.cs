using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using MatoIndustry.Common;
using MatoIndustry.Helper;
using MatoIndustry.Model;

namespace MatoIndustry.ViewModel
{
    public class UserProfilePageViewModel : ViewModelBase
    {

        public UserProfilePageViewModel()
        {
            Init();
            this.PropertyChanged += UserProfilePageViewModel_PropertyChanged;
        }

        private void UserProfilePageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrenTool) && CurrenTool != null)
            {
                var page = CurrenTool.NavigationPage;
                CommonHelper.GoNavigate(page);
                this.CurrenTool = null;
            }
        }

        private void Init()
        {
            ToolList = new List<SmallTool>()
           {
               new SmallTool(){ Code = "Timer", Icon = FaIcons.IconClockO,NavigationPage = "ToolTimerPage",Title= "计时器"},
               new SmallTool(){ Code = "Todo", Icon = FaIcons.IconShoppingBasket,NavigationPage = "ToolTodoPage",Title= "购物清单"}
           };
        }

        private List<SmallTool> _toolList;

        public List<SmallTool> ToolList
        {
            get { return _toolList; }
            set
            {
                _toolList = value;
                RaisePropertyChanged();
            }
        }

        private SmallTool _currentTool;

        public SmallTool CurrenTool
        {
            get { return _currentTool; }
            set
            {
                _currentTool = value;
                RaisePropertyChanged();
            }
        }



    }
}
