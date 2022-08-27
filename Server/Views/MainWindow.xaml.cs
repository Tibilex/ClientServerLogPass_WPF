using Server.Models;
using Server.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow s_window;
        private ServerViewModel _serverViewModel;

        public MainWindow()
        {
            InitializeComponent();
            s_window = this;
            _serverViewModel = new ServerViewModel();
            this.DataContext = _serverViewModel;
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
