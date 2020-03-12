using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WindowVisualTree.Annotations;

namespace WindowVisualTree
{
    public class MainWindowViewmodel : INotifyPropertyChanged
    {
        private readonly WindowService _windowService;
        private Window _selectedWindow;
        private TreeNode _selectedTreeNode;

        public MainWindowViewmodel(WindowService windowService)
        {
            _windowService = windowService;
            Windows.AddRange(_windowService.GetWindows());
        }

        public ObservableCollection<Window> Windows { get; } = new ObservableCollection<Window>();

        public string WindowTitle { get; set; }

        public Window SelectedWindow
        {
            get { return _selectedWindow;}
            set
            {
                _selectedWindow = value;

                WindowTitle = $"{_selectedWindow.Title} ({_selectedWindow.Handle}) - Class Name: {_selectedWindow.ClassName}";

                var treeNodes = _selectedWindow.GetChildControls();
                WindowControls.Clear();
                WindowControls.Add(treeNodes);

                OnPropertyChanged("WindowTitle");
                OnPropertyChanged("WindowControls");
            }
        }

        public TreeNode SelectedTreeNode
        {
            get
            {
                return _selectedTreeNode;
            }
            set
            {
                _selectedTreeNode = value;
                OnPropertyChanged("ControlName");
                OnPropertyChanged("ControlType");
                OnPropertyChanged("ControlBounds");
            }
        }

        public string ControlName => _selectedTreeNode?.Name;
        public string ControlType => _selectedTreeNode?.ControlType;

        public string ControlBounds
        {
            get
            {
                var bounds = _selectedTreeNode?.BoundingRectangle.ToString();
                return bounds;
            }
        }

        public ObservableCollection<TreeNode> WindowControls { get; } = new ObservableCollection<TreeNode>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}