using System;
using MatoIndustry.Common;
using MatoIndustry.Model;
using MatoIndustry.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToolTodoEditingPage : ContentView
    {
        private bool _isEdit = false;
        public event EventHandler<CommonFunctionEventArgs> OnFinished;


        public ToolTodoEditingPage(TodoItemInfo info)
        {
            InitializeComponent();
            if (info != null)
            {
                _isEdit = true;
                this.BindingContext = new ToolTodoEditingPageViewModel(info);
               
            }
            else
            {
                _isEdit = false;
                this.BindingContext = new ToolTodoEditingPageViewModel();
               
            }
        }


        private void SubmitButtonButton_OnClicked(object sender, EventArgs e)
        {
            var playlistFunctionPageViewModel = this.BindingContext as ToolTodoEditingPageViewModel;

            if (OnFinished != null && playlistFunctionPageViewModel != null)
                OnFinished(this, new CommonFunctionEventArgs(playlistFunctionPageViewModel.TodoItem, _isEdit ? "Edit" : "Create"));
        }

    }
}