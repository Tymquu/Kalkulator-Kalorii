using Kalkulator_Kalorii.MVVM.View;
using System.Windows;

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
    }
}
