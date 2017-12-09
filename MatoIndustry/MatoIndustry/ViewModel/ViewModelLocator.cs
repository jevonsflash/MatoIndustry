/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MatoIndustry"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MatoIndustry.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace MatoIndustry.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SearchPageViewModel>();
            SimpleIoc.Default.Register<UserProfilePageViewModel>();
            SimpleIoc.Default.Register<CategoryPageViewModel>();
            SimpleIoc.Default.Register<RecommendPageViewModel>();
            SimpleIoc.Default.Register<ToolTimerPageViewModel>();
            SimpleIoc.Default.Register<ToolTodoPageViewModel>();
            SimpleIoc.Default.Register<SettingAndAboutPageViewModel>();


        }

        public MainPageViewModel MainPage => ServiceLocator.Current.GetInstance<MainPageViewModel>();
        public SearchPageViewModel SearchPage => ServiceLocator.Current.GetInstance<SearchPageViewModel>();
        public UserProfilePageViewModel UserProfilePage => ServiceLocator.Current.GetInstance<UserProfilePageViewModel>();
        public CategoryPageViewModel CategoryPage => ServiceLocator.Current.GetInstance<CategoryPageViewModel>();
        public RecommendPageViewModel RecommendPage => ServiceLocator.Current.GetInstance<RecommendPageViewModel>();
        public ToolTimerPageViewModel ToolTimerPage => ServiceLocator.Current.GetInstance<ToolTimerPageViewModel>();
        public ToolTodoPageViewModel ToolTodoPage => ServiceLocator.Current.GetInstance<ToolTodoPageViewModel>();
        public SettingAndAboutPageViewModel SettingAndAboutPage => ServiceLocator.Current.GetInstance<SettingAndAboutPageViewModel>();


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}