using Kalkulator_Kalorii.MVVM.View;
using System.Windows;
using System.Windows.Input;

namespace Kalkulator_Kalorii
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Kalkulator kal = new Kalkulator();
            kal.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {          
            Nowy_uzytkownik nu = new Nowy_uzytkownik();
            nu.Show();
        }

        private void X_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                MaxButton.Content = "1";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                MaxButton.Content = "2";
            }

        }
    }
}
