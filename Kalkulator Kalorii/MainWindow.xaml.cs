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
            Nazwa_obecnego_uzytkownika.Content = ObecnyUzytkownik.wybrany_uzytkownik_nazwa;
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

            Wybor_uzytkownika.ItemsSource = dataset.Tables[0].DefaultView;
            Wybor_uzytkownika.DisplayMemberPath = "NazwaUzytkownika";
            Wybor_uzytkownika.SelectedValuePath = "UserID";
            Wybor_uzytkownika.SelectedIndex = -1;
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
            if (Wybor_uzytkownika.SelectedIndex >= 0)
            {
                int id = int.Parse(Wybor_uzytkownika.SelectedValue.ToString());
                ObecnyUzytkownik.wybrany_uzytkownik_id = id;
                ObecnyUzytkownik.wybrany_uzytkownik_nazwa = Wybor_uzytkownika.Text;
            }
        }

        private void Wybor_uzytkownika_MouseEnter(object sender, MouseEventArgs e)
        {
            Wczytaj_uzytkownika();
        }
    }
}
