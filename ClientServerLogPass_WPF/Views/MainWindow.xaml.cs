using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientServerLogPass_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow s_window;
        public MainWindow()
        {
            InitializeComponent();
            s_window = this;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                MainWindow.s_window.DragMove();
            }
        }

        private void CloseWindow(object sender, EventArgs e) { Application.Current.Shutdown(); }
        private void MinimiseWindow(object sender, EventArgs e) { this.WindowState = WindowState.Minimized; }
    }
}
