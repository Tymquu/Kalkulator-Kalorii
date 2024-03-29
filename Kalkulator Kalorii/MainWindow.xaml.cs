﻿using Kalkulator_Kalorii.MVVM.Model;
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
            string lokalizacja;
            DataBase db = new DataBase();
            lokalizacja = "C:\\baza.sqlite";

#if DEBUG
            lokalizacja = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\baza.sqlite";
#endif


            if (!File.Exists(lokalizacja))
            {
                db.Create_DB(lokalizacja);
            }

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
            if (ObecnyUzytkownik.wybrany_uzytkownik_nazwa == null)
            {
                MessageBox.Show("Aby przejśc dalej należy wybrać użytkownika lub stworzyć nowego!", "Uwaga");
            }
            else
            {
                this.Hide();
                Kalkulator kal = new Kalkulator();
                kal.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Nowy_uzytkownik nu = new Nowy_uzytkownik("Nowy");
            nu.Show();

        }     

        private void Wybor_uzytkownika_LostFocus(object sender, RoutedEventArgs e)
        {
            Nazwa_obecnego_uzytkownika.Content = Wybor_uzytkownika.Text;
            if (Wybor_uzytkownika.SelectedIndex >= 0)
            {
                int id = int.Parse(Wybor_uzytkownika.SelectedValue.ToString());
                ObecnyUzytkownik.wybrany_uzytkownik_id = id;
                DataBase db = new DataBase();
                string lokalizacja = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\baza.sqlite";
                conn = db.Connection(lokalizacja);
                conn.Open();
                User current_user = db.GetUserData(id);
                ObecnyUzytkownik.Wzrost = current_user.Wzrost;
                ObecnyUzytkownik.wybrany_uzytkownik_nazwa = current_user.NazwaUzytkownika;
                ObecnyUzytkownik.wybrany_uzytkownik_id = current_user.UserID;
                ObecnyUzytkownik.Plec = current_user.Plec;
                ObecnyUzytkownik.ObecnaWaga = current_user.ObecnaWaga;
                ObecnyUzytkownik.DocelowaWaga = current_user.DocelowaWaga;
                ObecnyUzytkownik.Wiek = current_user.Wiek;
                ObecnyUzytkownik.WodaNaDzien = current_user.WodaNaDzien;
            }
        }

        private void Wybor_uzytkownika_MouseEnter(object sender, MouseEventArgs e)
        {
            Wczytaj_uzytkownika();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Nowy_uzytkownik nu = new Nowy_uzytkownik("Edycja");
            nu.ShowDialog();
            Wybor_uzytkownika_LostFocus(sender, e);
        }
    }
}
