using System;
using System.Diagnostics;
using MatoIndustry.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
        }

        public RecipeDetailPage(string id) : this()
        {
            this.BindingContext = new RecipeDetailPageViewModel(id);
        }

        void webOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            LoadingLabel.IsVisible = true;
        }

        void webOnEndNavigating(object sender, WebNavigatedEventArgs e)
        {
            LoadingLabel.IsVisible = false;
        }
    }
}