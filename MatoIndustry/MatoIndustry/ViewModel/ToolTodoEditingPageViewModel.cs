using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatoIndustry.Model;

namespace MatoIndustry.ViewModel
{
    public class ToolTodoEditingPageViewModel : ViewModelBase
    {
        public ToolTodoEditingPageViewModel()
        {
            this.SubmitCommand = new RelayCommand(SubmitAction);
            this.TodoItem = new TodoItemInfo()
            {
                Status = TodoItemStatus.Pending,
                Amount = 1,
                CreateTime = DateTime.Now

            };

        }

        public ToolTodoEditingPageViewModel(TodoItemInfo todoItemInfo) : this()
        {
            this.TodoItem = todoItemInfo;
        }

        private void SubmitAction()
        {
        }

        private TodoItemInfo _todoItem;

        public TodoItemInfo TodoItem
        {
            get { return _todoItem; }
            set
            {
                _todoItem = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand SubmitCommand { get; set; }

    }
}
