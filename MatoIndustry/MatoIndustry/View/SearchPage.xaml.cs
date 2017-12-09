using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatoIndustry.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            this.IsFocusOnEntry = false;
        }

        public static readonly BindableProperty SearchWordProperty = BindableProperty.Create(nameof(SearchWord),
            typeof(string), typeof(SearchPage), propertyChanged: SearchWordChanged);

        private static void SearchWordChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var searchWord = newValue as string;
            if (!string.IsNullOrEmpty(searchWord))
            {
                var searchPage = bindable as SearchPage;
                if (searchPage != null)
                {
                    searchPage.SearchEntry.Text = searchWord;
                    searchPage.SearchButton.Command.Execute(null);
                }
            }
        }

        /// <summary>
        /// 搜索起始词
        /// </summary>
        public string SearchWord
        {
            get { return (string)GetValue(SearchWordProperty); }
            set { SetValue(SearchWordProperty, value); }
        }

        public static readonly BindableProperty IsFocusOnEntryProperty = BindableProperty.Create(nameof(IsFocusOnEntry),
            typeof(bool), typeof(SearchPage), propertyChanged: IsFocusOnEntryChanged, defaultValue: false);

        private static void IsFocusOnEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var isFocusOnEntry = (bool)newValue;

            var searchPage = bindable as SearchPage;
            if (isFocusOnEntry)
            {
                if (searchPage != null)
                {
                    searchPage.SearchEntry.Text = string.Empty;
                    searchPage.SearchEntry.Focus();
                }
            }
        }

        /// <summary>
        /// 设置是否为输入状态
        /// </summary>
        public bool IsFocusOnEntry
        {
            get { return (bool)GetValue(IsFocusOnEntryProperty); }
            set { SetValue(IsFocusOnEntryProperty, value); }
        }

        private void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //Debug.WriteLine("TextChanged");
            if (string.IsNullOrEmpty((sender as Entry).Text))
            {
                this.SearchResultLayout.IsVisible = false;
                this.SearchWordsLayout.IsVisible = true;
                this.SearchLabel.IsVisible = false;
            }
            else
            {
                this.SearchResultLayout.IsVisible = true;
                this.SearchWordsLayout.IsVisible = false;
                this.SearchLabel.IsVisible = true;


            }
        }
    }
}