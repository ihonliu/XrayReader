using System.Windows;
using System.Windows.Input;

namespace Xray.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DocumentViewerMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
