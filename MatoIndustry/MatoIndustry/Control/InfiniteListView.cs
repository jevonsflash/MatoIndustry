using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace MatoIndustry.Control
{
    public class InfiniteListView : ListView
    {
        /// <summary>
        /// 要求视图模型加载额外的数据绑定集合命令。
        /// </summary>
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create<InfiniteListView, ICommand>(bp => bp.LoadMoreCommand, default(ICommand));

        /// <summary>
        /// 当listview正在接近List的底部获取或设置调用的命令绑定，以加载更多的数据。
        /// </summary>
        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }


        public InfiniteListView()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }


        void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;

            if (items != null && e.Item == items[items.Count - 1])
            {
                if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                    LoadMoreCommand.Execute(null);
            }
        }


    }

}
