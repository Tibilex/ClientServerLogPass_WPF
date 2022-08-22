using ClientServerLogPass_WPF.ViewModels;
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
        private ClientViewModel _clientViewModel;
        public string PasswordString { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            s_window = this;
            _clientViewModel = new ClientViewModel();
            this.DataContext = _clientViewModel;
            loginMode.IsChecked = true;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Text == "") 
            { 
                loginCheck.Text = "Пустое поле";
                loginCheck.Foreground = new SolidColorBrush(Color.FromRgb(213, 65, 79)); 
                loginTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(213, 65, 79)); 

            }
            if (passwordBox.Password == "") 
            {
                passwordCheck.Text = "Пустое поле";
                passwordCheck.Foreground = new SolidColorBrush(Color.FromRgb(213, 65, 79));
                passwordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(213, 65, 79)); 

            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            _clientViewModel.Password = this.PasswordString;
        }

        private void loginMode_Click(object sender, RoutedEventArgs e)
        {            
            if (loginMode.IsChecked == true)
            {
                LogText.Foreground = new SolidColorBrush(Color.FromRgb(18, 170, 146));
                RegText.Foreground = new SolidColorBrush(Color.FromRgb(111, 118, 126));
            }
            else if(loginMode.IsChecked == false)
            {
                RegText.Foreground = new SolidColorBrush(Color.FromRgb(213, 65, 79));
                LogText.Foreground = new SolidColorBrush(Color.FromRgb(111, 118, 126));
            }
        }
    }
}
