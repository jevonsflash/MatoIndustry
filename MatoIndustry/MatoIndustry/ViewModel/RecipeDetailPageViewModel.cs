using System.Linq;
using GalaSoft.MvvmLight;
using MatoIndustry.Model;
using MatoIndustry.Server;

namespace MatoIndustry.ViewModel
{
    public class RecipeDetailPageViewModel : ViewModelBase
    {
        private readonly RecipeServer recipeServer = new RecipeServer();

        public RecipeDetailPageViewModel()
        {
            this.PropertyChanged += RecipeDetailPageViewModel_PropertyChanged;

        }

        public RecipeDetailPageViewModel(string id)
        {
            this.PropertyChanged += RecipeDetailPageViewModel_PropertyChanged;
            Id = id;
        }

        private async void RecipeDetailPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Id))
            {
                var cookdetail = await recipeServer.GetRecipeDetailEntity(Id);
                if (cookdetail.Result != null)
                {
                    this.CurrentCookDetail = new RecipeDetailInfo();
                    CurrentCookDetail.Name = "http://192.168.31.65:8008/pdfjs/web/viewer.html?file=/PDFs/test2.pdf";
                }

            }

        }


        private RecipeDetailInfo _currentCookDetail;

        public RecipeDetailInfo CurrentCookDetail
        {
            get { return _currentCookDetail; }
            set
            {
                _currentCookDetail = value;
                RaisePropertyChanged();
            }
        }

        private string _id;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }


    }
}
