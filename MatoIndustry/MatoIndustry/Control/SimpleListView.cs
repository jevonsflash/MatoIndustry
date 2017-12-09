using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MatoIndustry.Control
{

    public class SimpleListView : ContentView
    {
        readonly StackLayout _stack;

        public SimpleListView()
        {

            _stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            Content = _stack;
        }

        public IList<Xamarin.Forms.View> Children
        {
            get
            {
                return _stack.Children;
            }
        }

        private bool _layingOutChildren;


        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(SimpleListView),
                null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    ((SimpleListView)bindableObject).ItemsSourceChanged();
                }
            );

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


        public void ItemsSourceChanged()
        {
            if (ItemsSource == null)
                return;

            _stack.Children.Clear();
            foreach (var item in ItemsSource)
            {
                var view = (Xamarin.Forms.View)ItemTemplate.CreateContent();
                var bindableObject = view as BindableObject;
                if (bindableObject != null)
                    bindableObject.BindingContext = item;
                _stack.Children.Add(view);
            }
        }


        public DataTemplate ItemTemplate
        {
            get;
            set;
        }
    }

}
