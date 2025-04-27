
using System.Windows;
using System.Windows.Input;

namespace QSITThirdTask.View
{
    /// <summary>
    /// Interaction logic for QSITWindow.xaml
    /// </summary>
    public partial class QSITWindow : Window
    {
        public QSITWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
