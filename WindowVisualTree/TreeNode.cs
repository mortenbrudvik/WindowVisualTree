using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace WindowVisualTree
{
    public class TreeNode
    {
        private readonly AutomationElement _rootElement;

        public TreeNode(AutomationElement rootElement)
        {
            _rootElement = rootElement;
            Children = new ObservableCollection<TreeNode>();
        }

        public string Name => $"{_rootElement.Current.Name}";
        public string NameLong => $"{_rootElement.Current.Name} ({ControlType})";
        public string ControlType => _rootElement.Current.ControlType?.LocalizedControlType;
        public Rectangle BoundingRectangle
        {
            get
            {
                GetWindowRect(new HandleRef(this, Handle), out var rct);
                return rct;
            }
        }

        public IntPtr Handle => new IntPtr(_rootElement.Current.NativeWindowHandle);

        public AutomationElement.AutomationElementInformation ControlElement => _rootElement.Current;

        public ObservableCollection<TreeNode> Children { get; set; }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(HandleRef hWnd, out Rectangle lpRect);
    }
}