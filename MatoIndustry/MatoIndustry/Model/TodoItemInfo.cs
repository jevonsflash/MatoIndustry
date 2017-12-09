using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MatoIndustry.Model
{
    public class TodoItemInfo : ViewModelBase, IBasicInfo
    {
        public TodoItemInfo()
        {
            Id = Guid.NewGuid().ToString("N");
            this.SwitchCommand=new RelayCommand(SwitchAction);
        }

        private void SwitchAction()
        {
            if (Status==TodoItemStatus.Pending)
            {
                Status=TodoItemStatus.Completed;
            }

            else if (Status==TodoItemStatus.Completed)
            {
                Status=TodoItemStatus.Deleted;
            }
            else
            {
                Status=TodoItemStatus.Completed;
            }
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle {
            get { return string.IsNullOrEmpty(Note) ? CreateTime.ToString("yyyy-M-d dddd") : Note; }
        }
        public int Amount { get; set; }
        public string Note { get; set; }
        private TodoItemStatus _status;
        public TodoItemStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }
        public string RecipeId { get; set; }
        public DateTime CreateTime { get; set; }


        public RelayCommand SwitchCommand { get; set; }
    }

    public enum TodoItemStatus
    {
        Deleted, Completed, Pending
    }

    
}
