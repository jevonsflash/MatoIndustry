using System.Collections.Generic;
using GalaSoft.MvvmLight;
using MatoIndustry.Model;
using Xamarin.Forms;

namespace MatoIndustry.ViewModel
{
    public class ClassifyItemViewModel : ViewModelBase
    {

        public ClassifyItemViewModel()
        {
            //this.SwitchCommand = new RelayCommand(SwitchAction);
            IsOpen = false;
        }

        //public Action<ClassifyItemViewModel> SwitchActionEx { get; set; }

        //private void SwitchAction()
        //{
        //    var tempIsOpen = IsOpen;
        //    SwitchActionEx?.Invoke(this);
        //    if (!tempIsOpen)
        //    {
        //        this.IsOpen = true;

        //    }

        //}

        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                _isOpen = value;
                RaisePropertyChanged();
            }
        }

        private Color _tintColor;

        public Color TintColor
        {
            get { return _tintColor; }
            set
            {
                _tintColor = value;
                RaisePropertyChanged();

            }
        }


        private CategoryInfo _currentClassify;

        public CategoryInfo CurrentClassify
        {
            get { return _currentClassify; }
            set
            {
                _currentClassify = value;
                RaisePropertyChanged();

            }
        }

        private IList<ClassifyItemViewModel> _subClassifies;

        public IList<ClassifyItemViewModel> SubClassifies
        {
            get { return _subClassifies; }
            set
            {
                _subClassifies = value;
                RaisePropertyChanged();

            }
        }

       // public RelayCommand SwitchCommand { get; set; }
    }

}
