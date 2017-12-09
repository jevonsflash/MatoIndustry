using System;
using MatoIndustry.Common;
using MatoIndustry.Helper;
using MatoIndustry.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecommendPage : ContentPage
    {
        public RecommendPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            (this.BindingContext as RecommendPageViewModel).RecommendItemClickedAction();
        }

        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {
            CommonHelper.GoNavigate("SearchPage", NavigationType.SwitchTab);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            CommonHelper.GoNavigate("CategoryPage", NavigationType.SwitchTab);
        }
    }
}