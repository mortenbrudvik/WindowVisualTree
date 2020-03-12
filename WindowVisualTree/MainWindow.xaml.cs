using System.Windows;

namespace WindowVisualTree
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var windowService = new WindowService();
            DataContext = new MainWindowViewmodel(windowService);
            Loaded += (sender, args) => { DataContext = new MainWindowViewmodel(windowService); };
        }
    }
}
