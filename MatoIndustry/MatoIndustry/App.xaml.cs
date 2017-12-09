using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using MatoIndustry.Common;
using MatoIndustry.Helper;
using MatoIndustry.View;
using MatoIndustry.ViewModel;
using Xamarin.Forms;

namespace MatoIndustry
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(CurrentMainPage)
            {
                BarBackgroundColor = Color.SaddleBrown,
                BarTextColor = Color.White,
            };
            Messenger.Default.Register<WindowArg>(this, TokenHelper.WindowToken, HandleWindowResult);
        }

        private async void HandleWindowResult(WindowArg obj)
        {

            ViewModelLocator.Cleanup();

            if (obj.Type == NavigationType.NavigateTo)
            {


                var navigatePage = GetPageInstance(obj.Name, obj.Args, obj.ToolbarItems);
                if (navigatePage != null)
                {
                    await MainPage.Navigation.PushAsync(navigatePage);

                }

            }

            else if (obj.Type == NavigationType.NavigateBack)
            {
                await MainPage.Navigation.PopAsync();

            }
            else if (obj.Type == NavigationType.PushModal)
            {
                var toolbars = obj.ToolbarItems;
                toolbars.Add(new ToolbarItem("close", "close",
                    () =>
                    {
                        MainPage.Navigation.PopModalAsync();
                    }));
                var navigatePage = GetPageInstance(obj.Name, obj.Args, toolbars);
                if (navigatePage != null)
                {
                    await MainPage.Navigation.PushModalAsync(navigatePage);

                }
            }
            else if (obj.Type == NavigationType.PopModal)
            {
                await MainPage.Navigation.PopModalAsync();

            }

            else if (obj.Type == NavigationType.SwitchTab)
            {
                var tabbedPage = (CurrentMainPage as TabbedPage);
                var targetPage = tabbedPage.Children.FirstOrDefault(c => c.GetType().Name == obj.Name);
                if (obj.Name == nameof(SearchPage))
                {
                    var searchPage = targetPage as SearchPage;
                    if (searchPage != null)

                        if (obj.Args != null)
                        {
                            searchPage.SearchWord = obj.Args[0] as string;
                        }
                        else
                        {
                            searchPage.IsFocusOnEntry = false;
                            searchPage.IsFocusOnEntry = true;
                        }
                }
                tabbedPage.CurrentPage = targetPage;
            }

        }
        private static Xamarin.Forms.Page GetPageInstance(string obj, object[] args, IList<ToolbarItem> barItem = null)
        {
            Xamarin.Forms.Page result = null;
            var namespacestr = typeof(App).Namespace;
            Type pageType = Type.GetType(namespacestr + ".View." + obj, false);
            if (pageType != null)
            {
                try
                {
                    var pageObj = Activator.CreateInstance(pageType, args) as Xamarin.Forms.Page;

                    if (barItem != null && barItem.Count > 0)
                    {
                        foreach (var toolbarItem in barItem)
                        {
                            pageObj.ToolbarItems.Add(toolbarItem);

                        }
                    }

                    result = pageObj;

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);

                }
            }
            return result;
        }

        private Xamarin.Forms.Page _currentMainPage;

        public Xamarin.Forms.Page CurrentMainPage
        {
            get
            {
                if (_currentMainPage == null)
                {
                    _currentMainPage = new MainPage();
                }
                return _currentMainPage;

            }
            set { _currentMainPage = value; }
        }

    }
}
