using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MatoIndustry.Control
{

    public class MainGridView : ScrollView
    {
        private readonly Grid _stack;
        private readonly Grid _stackHeader;
        private readonly Grid _stackFooter;
        private readonly StackLayout _layout;

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MainGridView));
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MainGridView));
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(Xamarin.Forms.View), typeof(MainGridView), propertyChanged: HeaderPropertyChanged);
        public static readonly BindableProperty FooterProperty = BindableProperty.Create(nameof(Footer), typeof(Xamarin.Forms.View), typeof(MainGridView), propertyChanged: FooterPropertyChanged);

        private static void HeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as MainGridView;
            if (newValue != null)
            {
                control._stackHeader.Children.Add(control.Header);
                control.ScrollToAsync(0, 0, false);
            }
        }

        private static void FooterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as MainGridView;
            if (newValue != null)
            {
                control._stackFooter.Children.Add(control.Footer);
                control.ScrollToAsync(0, 0, false);
            }

        }

        private int _maxColumns = 2;
        private float _tileHeight = 100;
        private int _selectedIndex;

        public MainGridView()
        {

            Orientation = ScrollOrientation.Vertical;

            _stack = new Grid();
            _stackHeader = new Grid();
            _stackFooter = new Grid();
            _layout = new StackLayout();

            for (var i = 0; i < MaxColumns; i++)
            {
                _stack.ColumnDefinitions.Add(new ColumnDefinition());
            }
            _layout.Children.Add(_stackHeader);
            _layout.Children.Add(_stack);
            _layout.Children.Add(_stackFooter);

            Content = _layout;
        }
        public IList<Xamarin.Forms.View> Children
        {
            get
            {
                return _stack.Children;
            }
        }
        public DataTemplate ItemTemplate { get; set; }

        public int MaxColumns
        {
            get { return _maxColumns; }
            set { _maxColumns = value; }
        }

        public float TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; }
        }
        public Xamarin.Forms.View Header
        {
            get { return (Xamarin.Forms.View)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public Xamarin.Forms.View Footer
        {
            get { return (Xamarin.Forms.View)GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
                BindableProperty.Create(
                    nameof(ItemsSource),
                    typeof(IList),
                    typeof(MainGridView),
                    null,
                 propertyChanging: (bindableObject, oldValue, newValue) =>
                {
                    ((MainGridView)bindableObject).ItemsSourceChanging();
                },
                    propertyChanged: (bindableObject, oldValue, newValue) =>
                    {
                        ((MainGridView)bindableObject).ItemsSourceChanged();
                    }
                );


        private void ItemsSourceChanging()
        {
            if (ItemsSource == null) return;
            _selectedIndex = ItemsSource.IndexOf(SelectedItem);

        }

        private async void ItemsSourceChanged()
        {
            if (ItemsSource != null)
            {
                await BuildTiles(ItemsSource);

            }
        }

        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        private async Task BuildTiles(IList tiles)
        {
            // Wipe out the previous row definitions if they're there.
            if (_stack.RowDefinitions.Any())
            {
                _stack.RowDefinitions.Clear();
            }
            _stack.Children.Clear();
            var enumerable = tiles;
            var numberOfRows = Math.Ceiling(enumerable.Count / (float)MaxColumns);
            for (var i = 0; i < numberOfRows; i++)
            {
                _stack.RowDefinitions.Add(new RowDefinition { Height = TileHeight });
            }

            for (var index = 0; index < enumerable.Count; index++)
            {
                var column = index % MaxColumns;
                var row = (int)Math.Floor(index / (float)MaxColumns);

                var tile = await BuildTile(enumerable[index]);
                var bindableObject = tile as BindableObject;
                if (bindableObject != null)
                    bindableObject.BindingContext = enumerable[index];
                _stack.Children.Add(tile, column, row);
            }
            if (_selectedIndex >= 0)
            {
                SelectedIndex = _selectedIndex;
            }
        }

        private async Task<Xamarin.Forms.View> BuildTile(object item1)
        {
            return await Task.Run(() =>
            {
                var buildTile = (Xamarin.Forms.View)ItemTemplate.CreateContent();
                buildTile.BackgroundColor = Color.White;
                buildTile.GestureRecognizers.Add(new TapGestureRecognizer(TapGestureRecognizer_Tapped)
                {
                    Command = Command,
                    CommandParameter = item1
                });


                return buildTile;
            });
        }


        private void TapGestureRecognizer_Tapped(Xamarin.Forms.View sender, object arg2)
        {
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!");
            var view = sender as Xamarin.Forms.View;
            if (view != null) SelectedItem = view.BindingContext;
        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(MainGridView),
                0,
                BindingMode.TwoWay,
                propertyChanged: async (bindable, oldValue, newValue) =>
                {
                    await ((MainGridView)bindable).UpdateSelectedItem();
                }
            );

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }
        async Task UpdateSelectedItem()
        {
            await Task.Delay(300);
            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
        }
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(MainGridView),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((MainGridView)bindable).UpdateSelectedIndex();
                }
            );

        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        void UpdateSelectedIndex()
        {
            if (SelectedItem == BindingContext) return;

            SelectedIndex = Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(SelectedItem);
        }

    }
}