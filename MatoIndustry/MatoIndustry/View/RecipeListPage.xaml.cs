using MatoIndustry.Model;
using MatoIndustry.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeListPage : ContentPage
    {
        public RecipeListPage()
        {
            InitializeComponent();
        }

        public RecipeListPage(CategoryInfo info) : this()
        {
            this.BindingContext = new RecipeListPageViewModel(info);

        }

    }
}