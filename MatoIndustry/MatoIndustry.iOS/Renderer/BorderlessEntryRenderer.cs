using MatoIndustry.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Entry), typeof(BorderlessEntryRenderer))]
namespace MatoIndustry.iOS.Renderer
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
                return;

            this.Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}

