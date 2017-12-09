using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatoIndustry.Helper;

namespace MatoIndustry.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainPageViewModel()
        {
            this.GoAboutPageCommand = new RelayCommand(GoAboutPageAction);

        }

        private void GoAboutPageAction()
        {
            CommonHelper.GoNavigate("SettingAndAboutPage");
        }

        public RelayCommand GoAboutPageCommand { get; set; }
    }
}