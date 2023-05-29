using Kalkulator_Kalorii.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
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
using System.Windows.Shapes;

namespace Kalkulator_Kalorii.MVVM.View
{
    /// <summary>
    /// Interaction logic for Kalkulator.xaml
    /// </summary>
    public partial class Kalkulator : Window
    {
        private SQLiteConnection conn;
        public Kalkulator()
        {
            InitializeComponent();
            Nazwa_obecnego_uzytkownika.Content = ObecnyUzytkownik.wybrany_uzytkownik_nazwa;
            data.SelectedDate = DateTime.Today;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Historia ht = new Historia();
            ht.Show();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

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

        private void Click_dodanie_posilku(object sender, RoutedEventArgs e)
        {
            DataBase db = new DataBase();
            string lokalizacja = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\baza.sqlite";
            conn = db.Connection(lokalizacja);
            conn.Open();
            int h_id = db.GetHistoriaID();
            Model.Historia s = new Model.Historia
            {
                HistoriaID = h_id,
                UserID = ObecnyUzytkownik.wybrany_uzytkownik_id,
                PosilekTyp = Typ_posilku.Text,
                DanyPosilek = Dany_posilek.Text,
                WagaPosilku = Convert.ToDecimal(Waga_posilku.Text),
                KalorycznoscPosilku = Convert.ToInt32(kcal_posilku.Text),
                Woda = Convert.ToDecimal(Woda_ml_.Text),
                Aktywnosc = Nazwa_aktywnosci.Text,
                CzasAktywnosci = Convert.ToDecimal(czas_aktywnosci.Text),
                Data = data.SelectedDate.Value
            };

            db.InsertHistoria(s);

        }
    }
}
