using System;
using System.Collections.Generic;
using System.Data;
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
using Kalkulator_Kalorii.MVVM.Model;

namespace Kalkulator_Kalorii.MVVM.View
{
    /// <summary>
    /// Interaction logic for Historia.xaml
    /// </summary>
    public partial class Historia : Window
    {
        private SQLiteConnection conn;
        private SQLiteCommand command;
        private string sql;

        public Historia()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            Historia_data.SelectedDate = DateTime.Today;

            string lokalizacja;
            DataBase db = new DataBase();
            lokalizacja = "C:\baza.sqlite";

            #if DEBUG
            lokalizacja = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\baza.sqlite";
            #endif


            if (!File.Exists(lokalizacja))
            {
                db.Create_DB(lokalizacja);
            }

            conn = db.Connection(lokalizacja);
            conn.Open();

            sql = "SELECT PosilekTyp,DanyPosilek,WagaPosilku,KalorycznoscPosilku,Woda,Aktywnosc,CzasAktywnosci FROM Historia";
            command = new SQLiteCommand(sql, conn);

            Fill(command);
            conn.Close();
        }

        public void Fill(SQLiteCommand command)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Historia_kalorii.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
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

        private void Historia_data_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
