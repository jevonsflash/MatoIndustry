using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupView : ContentView
    {
        public PopupView()
        {
            InitializeComponent();
        }
        public void ShowPopup(Xamarin.Forms.View FunctionPage)
        {
            Popup.Children.Add(FunctionPage);
            Popup.InputTransparent = false;
            this.IsVisible = true;
        }

        public void HidePopup()
        {
            Popup.Children.Clear();
            Popup.InputTransparent = true;
            this.IsVisible = false;
        }

        #region IsShowClose
        public static readonly BindableProperty IsShowCloseProperty = BindableProperty.Create(nameof(IsShowClose),
            typeof(bool), typeof(PopupView), propertyChanged: IsShowClosePropertyChanged, defaultValue: true);

        private static void IsShowClosePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var isShow = (bool)newvalue;
            var control = bindable as PopupView;
            control.CloseButtonImg.IsVisible = isShow;
            control.CloseButtonLayout.IsVisible = isShow;
        }

        public bool IsShowClose
        {
            get { return (bool)GetValue(IsShowCloseProperty); }
            set { SetValue(IsShowCloseProperty, value); }
        }
        #endregion

        private void Button_OnClicked(object sender, EventArgs e)
        {
            HidePopup();
        }
    }
}