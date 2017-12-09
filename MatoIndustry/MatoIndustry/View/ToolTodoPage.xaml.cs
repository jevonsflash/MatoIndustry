using System;
using MatoIndustry.Common;
using MatoIndustry.Model;
using MatoIndustry.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToolTodoPage : ContentPage
    {
        public ToolTodoPage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {

            var todoPage = new ToolTodoEditingPage(null);
            todoPage.OnFinished += TodoPage_OnFinished;
            this.Popup.ShowPopup(todoPage);
        }

        private void TodoPage_OnFinished(object sender, CommonFunctionEventArgs e)
        {
            var newItem = e.Info as TodoItemInfo;
            if (string.IsNullOrEmpty(newItem.Title))
            {
                return;

            }
            (this.BindingContext as ToolTodoPageViewModel).AddItem(newItem);
            this.Popup.HidePopup();
        }

        private void ToolbarItem2_OnActivated(object sender, EventArgs e)
        {
            (this.BindingContext as ToolTodoPageViewModel).MarkAllCompletedAsDeleted();

        }

        private void ToolTodoPage_OnDisappearing(object sender, EventArgs e)
        {
            (this.BindingContext as ToolTodoPageViewModel).RemoveAllDeleted();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}