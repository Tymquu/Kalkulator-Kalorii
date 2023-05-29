using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Kalkulator_Kalorii.MVVM.Model;

namespace Kalkulator_Kalorii.MVVM.View
{
    /// <summary>
    /// Interaction logic for Wybor_uzytkownika.xaml
    /// </summary>
    public partial class Nowy_uzytkownik : Window
    {
        private SQLiteConnection conn;

        public Nowy_uzytkownik()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            this.Close();
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

        private void UserSave_Click(object sender, RoutedEventArgs e)
        {
            DataBase db = new DataBase();
            string lokalizacja = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\baza.sqlite";
            conn = db.Connection(lokalizacja);
            conn.Open();
            int u_id = db.GetUserID();
            User s = new User
            {
                UserID = u_id,
                NazwaUzytkownika = nazwa_uzytkownika.Text,
                Wzrost = Convert.ToInt32(wzrost.Text),
                Plec = PlecCmb.Text,
                ObecnaWaga = Convert.ToDecimal(obecna_waga.Text),
                DocelowaWaga = Convert.ToDecimal(docelowa_waga.Text)
            };

            db.InsertUser(s);
            this.Close();
        }

        private void InsertPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
