using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
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

        public string AllPropertiesText
        {
            get
            {
                var properties = _rootElement.GetSupportedProperties();
                var elementProperties = new List<ElementProperty>();
                foreach (var property in properties)
                {
                    var propValue = _rootElement.GetCurrentPropertyValue(property)?.ToString();
                    elementProperties.Add(new ElementProperty(property.ProgrammaticName, propValue, property.Id));
                }

                var builder = new StringBuilder();
                foreach (var elementProperty in elementProperties)
                {
                    builder.Append(elementProperty + Environment.NewLine);
                }

                return builder.ToString();
            }
        }

        public ObservableCollection<TreeNode> Children { get; set; }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(HandleRef hWnd, out Rectangle lpRect);
    }

    struct ElementProperty
    {
        public string Name { get; }
        public string Value { get; }
        public int Id { get; }

        public ElementProperty(string name, string value, int id)
        {
            Name = name;
            Value = value;
            Id = id;
        }

        public override string ToString()
        {
            return $"{Id} : {Name} : {Value}";
        }
    }
}