using System;
using System.Windows.Automation;

namespace WindowVisualTree
{
    public class Window 
    {
        private readonly AutomationElement _winElement;

        public Window(AutomationElement winElement)
        {
            _winElement = winElement;
        }

        public string Title => GetTitle();
        public IntPtr Handle => GetHandle();
        public string ClassName => GetClassName();

        public TreeNode GetChildControls()
        {
            var visualTreeService = new VisualTreeService();
            var nodes = visualTreeService.Convert(_winElement);

            return nodes;
        }

        private string GetTitle()
        {
            var name = _winElement?.Current.Name;
            return  string.IsNullOrWhiteSpace(name) ? "No name" : name;
        }

        private IntPtr GetHandle()
        {
            var handle = _winElement?.Current.NativeWindowHandle;
            return handle == null ? IntPtr.Zero : new IntPtr((int)handle);
        }

        private string GetClassName()
        {
            return _winElement.Current.ClassName;
        }
    }
}