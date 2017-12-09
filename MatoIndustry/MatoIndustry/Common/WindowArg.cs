using System.Collections.Generic;
using Xamarin.Forms;

namespace MatoIndustry.Common
{
    public class WindowArg
    {
        public WindowArg(string name, object[] args = null, NavigationType type = NavigationType.NavigateTo, IList<ToolbarItem> toolbarItems = null)
        {
            Name = name;
            Args = args;
            Type = type;
            ToolbarItems = toolbarItems;

        }
        public string Name { get; set; }
        public object[] Args { get; set; }
        public NavigationType Type { get; set; }
        public IList<ToolbarItem> ToolbarItems { get; set; }
    }

    public enum NavigationType
    {
        NavigateTo, PushModal, PopModal, NavigateBack,SwitchTab
    }
}
