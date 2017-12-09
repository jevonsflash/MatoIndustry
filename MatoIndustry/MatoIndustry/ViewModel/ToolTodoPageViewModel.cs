using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using GalaSoft.MvvmLight;
using MatoIndustry.Model;
using MatoIndustry.Server;

namespace MatoIndustry.ViewModel
{
    public class ToolTodoPageViewModel : ViewModelBase
    {

        public ToolTodoPageViewModel()
        {
            Init();
            this.TodoList.CollectionChanged += TodoList_CollectionChanged;

        }

        private async void TodoList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            await TodoServer.Current.SaveTodoList(TodoList.ToList());
        }

        private async void Init()
        {
            TodoList = new ObservableCollection<TodoItemInfo>(await TodoServer.Current.GetTodoList());
        }

        private ObservableCollection<TodoItemInfo> _todoList;

        public ObservableCollection<TodoItemInfo> TodoList
        {
            get
            {
                if (_todoList == null)
                {
                    _todoList = new ObservableCollection<TodoItemInfo>();
                }
                return _todoList;

            }
            set
            {
                _todoList = value;
                RaisePropertyChanged();
            }
        }

        private TodoItemInfo _currenTodoItem;

        public TodoItemInfo CurrenTodoItem
        {
            get { return _currenTodoItem; }
            set
            {
                _currenTodoItem = value;
                RaisePropertyChanged();
            }
        }

        public void AddItem(TodoItemInfo newItem)
        {
            TodoList.Add(newItem);
        }
        public void RemoveItems(TodoItemInfo item)
        {
            TodoList.Remove(item);
        }
        public void RemoveItems(string id)
        {
            TodoList.Remove(TodoList.FirstOrDefault(c => c.Id == id));
        }
        public void RemoveAllDeleted()
        {
            var deletedItemId = TodoList.Where(c => c.Status == TodoItemStatus.Deleted).Select(c => c.Id).ToList();
            foreach (var currentId in deletedItemId)
            {
                RemoveItems(currentId);
            }
        }

        public void MarkAllCompletedAsDeleted()
        {
            var deletedItem = TodoList.Where(c => c.Status == TodoItemStatus.Pending);
            foreach (var todoItemInfo in deletedItem)
            {
                todoItemInfo.Status = TodoItemStatus.Completed;
            }
        }
    }
}
