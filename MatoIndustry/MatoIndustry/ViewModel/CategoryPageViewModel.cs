using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight;
using MatoIndustry.Helper;
using MatoIndustry.Model;
using MatoIndustry.Server;
using Xamarin.Forms;

namespace MatoIndustry.ViewModel
{
    public class CategoryPageViewModel : ViewModelBase
    {
        private RecipeServer recipeServer = new RecipeServer();

        public CategoryPageViewModel()
        {
            this.PropertyChanged += CategoryPageViewModel_PropertyChanged;
            this.InitClassifyList();
        }


        private void CategoryPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ClassifyList) && ClassifyList != null)
            {
                foreach (var classifyItemViewModel in ClassifyList)
                {
                    classifyItemViewModel.IsOpen = false;
                }
                InitSubClassify();
            }

            else if (e.PropertyName == nameof(CurrentClassify) && CurrentClassify != null)
            {
                var index = ClassifyList.IndexOf(CurrentClassify);
                for (var i = 0; i < ClassifyList.Count; i++)
                {
                    ClassifyList[i].IsOpen = i == index;
                }
            }
            else if (e.PropertyName == nameof(SubCurrentClassify) && SubCurrentClassify != null)
            {
                CommonHelper.GoNavigate("RecipeListPage", args: new[] { SubCurrentClassify.CurrentClassify });

            }
        }

        public void InitSubClassify()
        {
            if (ClassifyList != null && ClassifyList.Count > 0)
            {
                this.CurrentClassify = ClassifyList.FirstOrDefault();

            }
        }

        public async void InitClassifyList()
        {
            var source = await recipeServer.GetRecipeCategoryEntity();
            if (source.Result != null)
            {
                this.ClassifyList = source.Result.Childs.Select(c => new ClassifyItemViewModel()
                {
                    TintColor = Color.CornflowerBlue,
                    CurrentClassify = new CategoryInfo()
                    {
                        CtgId = c.CategoryInfo.CtgId,
                        Name = c.CategoryInfo.Name.Substring(1, 2),
                        ParentId = c.CategoryInfo.ParentId
                    },
                    SubClassifies = c.Childs.Select(d => new ClassifyItemViewModel()
                    {
                        TintColor = Color.CornflowerBlue,
                        CurrentClassify = d.CategoryInfo,

                    }).ToList(),

                }).ToList();

            }
        }
        private ClassifyItemViewModel _currentClassify;

        public ClassifyItemViewModel CurrentClassify
        {
            get { return _currentClassify; }
            set
            {
                _currentClassify = value;
                RaisePropertyChanged();
            }
        }
        private ClassifyItemViewModel _subCurrentClassify;

        public ClassifyItemViewModel SubCurrentClassify
        {
            get { return _subCurrentClassify; }
            set
            {
                _subCurrentClassify = value;
                RaisePropertyChanged();
            }
        }
        private List<ClassifyItemViewModel> _classifyList;

        public List<ClassifyItemViewModel> ClassifyList
        {
            get
            {
                if (_classifyList == null)
                {
                    _classifyList = new List<ClassifyItemViewModel>();
                }
                return _classifyList;
            }
            set
            {
                _classifyList = value;
                RaisePropertyChanged();
            }
        }
    }


}
