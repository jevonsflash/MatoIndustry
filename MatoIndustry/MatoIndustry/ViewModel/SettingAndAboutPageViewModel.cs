using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace MatoIndustry.ViewModel
{
    public class SettingAndAboutPageViewModel : ViewModelBase
    {
        public SettingAndAboutPageViewModel()
        {
            this.GoBackCommand = new RelayCommand(GoBackAction);
            this.GoUriCommand = new RelayCommand<string>(GoUriAction);

            
            Version = (string)App.Current.Resources["Version"];
            Brief = "工控";
            Introduction = "";

            StrUpdate += ("暂无更新");
            //StrUpdate+=("2.手势功能来了,在首页您可以通过滑动专辑图片进行上一曲/下一曲操作");
            //StrUpdate+=("3.全局歌曲控制栏,您可以在任意页面查看和控制曲目");
            //StrUpdate+=("4.搜索功能大升级,支持联合搜索");
            //StrUpdate+=("5.全新队列功能,支持自定义播放队列，歌曲自由排序");
            //StrUpdate+=("6.全新的音乐库管理逻辑,您可以自由编辑歌曲信息，管理库，添加星级评价等");
            //StrUpdate+=("7.UI调整,支持主题色随主题背景更改");

            StrTips = "";
        }

        private void GoUriAction(object obj)
        {
            var uri = obj.ToString();
            UriBuilder uriSite = new UriBuilder(uri);
            Device.OpenUri(uriSite.Uri);
        }

        private async void GoBackAction()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private string strUpdate;

        public string StrUpdate
        {
            get { return strUpdate; }
            set
            {
                strUpdate = value;
                base.RaisePropertyChanged();
            }
        }
        private string strTips;

        public string StrTips
        {
            get { return strTips; }
            set
            {
                strTips = value;
                base.RaisePropertyChanged();
            }
        }

        private string version;

        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                base.RaisePropertyChanged();
            }
        }

        private string introduction;

        public string Introduction
        {
            get { return introduction; }
            set
            {
                introduction = value;
                base.RaisePropertyChanged();
            }
        }

        private string brief;

        public string Brief
        {
            get { return brief; }
            set
            {
                brief = value;
                base.RaisePropertyChanged();
            }
        }
        private bool _isAutoGA;

        public bool IsAutoGA
        {
            get { return _isAutoGA; }
            set { _isAutoGA = value; }
        }


        public RelayCommand GoBackCommand { get; set; }


        public RelayCommand<string> GoUriCommand { get; set; }


    }
}
