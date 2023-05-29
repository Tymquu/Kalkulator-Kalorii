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

        string Operacja;

        public Nowy_uzytkownik(string O)
        {
            InitializeComponent();
            Operacja = O;

            if (Operacja == "Edycja")
            {
                Edycja();
            }
        }

        private void Edycja()
        {
            wzrost.Text = ObecnyUzytkownik.Wzrost.ToString();
            nazwa_uzytkownika.Text = ObecnyUzytkownik.wybrany_uzytkownik_nazwa;
            wiek.Text = ObecnyUzytkownik.Wiek.ToString();
            PlecCmb.Text = ObecnyUzytkownik.Plec;
            obecna_waga.Text = ObecnyUzytkownik.ObecnaWaga.ToString();
            docelowa_waga.Text = ObecnyUzytkownik.DocelowaWaga.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UserSave_Click(object sender, RoutedEventArgs e)
        {
            
            DataBase db = new DataBase();
            string lokalizacja = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\baza.sqlite";
            conn = db.Connection(lokalizacja);
            conn.Open();

            if (Operacja == "Nowy")
            {
                int u_id = db.GetUserID();
                User s = new User
                {
                    UserID = u_id,
                    NazwaUzytkownika = nazwa_uzytkownika.Text,
                    Wzrost = Convert.ToInt32(wzrost.Text),
                    Plec = PlecCmb.Text,
                    ObecnaWaga = Convert.ToDecimal(obecna_waga.Text),
                    DocelowaWaga = Convert.ToDecimal(docelowa_waga.Text),
                    Wiek = Convert.ToInt32(wiek.Text)
                };

                db.InsertUser(s);
            }
            if (Operacja == "Edycja")
            {
                User s = new User
                {
                    UserID = ObecnyUzytkownik.wybrany_uzytkownik_id,
                    NazwaUzytkownika = nazwa_uzytkownika.Text,
                    Wzrost = Convert.ToInt32(wzrost.Text),
                    Plec = PlecCmb.Text,
                    ObecnaWaga = Convert.ToDecimal(obecna_waga.Text),
                    DocelowaWaga = Convert.ToDecimal(docelowa_waga.Text),
                    Wiek = Convert.ToInt32(wiek.Text)
                };
                db.UpdateUserData(s);
            }
            this.Close();
        }

        private void InsertPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
