using System;
using MatoIndustry.Server;
using MatoIndustry.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToolTimerPage : ContentPage
    {
        public ToolTimerPage()
        {
            InitializeComponent();
            Appearing += ToolTimerPage_Appearing;
            this.TipLabel.Text = TpiServer.GetTip();
        }

        private void ToolTimerPage_Appearing(object sender, EventArgs e)
        {
            var toolTimerPageViewModel = this.BindingContext as ToolTimerPageViewModel;
            toolTimerPageViewModel.RaisePropertyChanged(nameof(toolTimerPageViewModel.IsRunning));
        }
    }
}