using Xamarin.Forms;

namespace MatoIndustry.Control
{
    public class LargeImage : Image
    {
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create("ImageSource", typeof(string), typeof(LargeImage), default(string), propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (Device.OS != TargetPlatform.Android)
                {
                    var image = (LargeImage)bindable;

                    var baseImage = (Image)bindable;
                    baseImage.Source = image.ImageSource;
                }
            });

        public string ImageSource
        {
            get { return GetValue(ImageSourceProperty) as string; }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}
