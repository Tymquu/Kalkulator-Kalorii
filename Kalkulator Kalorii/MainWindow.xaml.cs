using Kalkulator_Kalorii.MVVM.Model;
using Kalkulator_Kalorii.MVVM.View;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kalkulator_Kalorii
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteConnection conn;
        public MainWindow()
        {
            InitializeComponent();                   
        }

        private void Wczytaj_uzytkownika()
        {
            DataBase db = new DataBase();
            string lokalizacja = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\baza.sqlite";
            conn = db.Connection(lokalizacja);
            conn.Open();
            SQLiteDataAdapter sda = db.ListaUzytkownikow();

            DataSet dataset = new DataSet();
            sda.Fill(dataset);

            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = row["NazwaUzytkownika"].ToString();
                Wybor_uzytkownika.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Kalkulator kal = new Kalkulator();
            kal.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Nowy_uzytkownik nu = new Nowy_uzytkownik();
            nu.Show();

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
                MaxButton.Content = "◰";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                MaxButton.Content = "□";
            }

        }

        private void Wybor_uzytkownika_LostFocus(object sender, RoutedEventArgs e)
        {
            Nazwa_obecnego_uzytkownika.Content = Wybor_uzytkownika.Text;
            //ObecnyUzytkownik.wybrany_uzytkownik = 
        }    

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            Wczytaj_uzytkownika();
        }
    }
}
