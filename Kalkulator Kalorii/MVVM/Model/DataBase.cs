using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Kalkulator_Kalorii.MVVM.Model
{
    public class DataBase
    {
        private SQLiteConnection conn;
        private string sql;
        private SQLiteCommand command;

        public SQLiteConnection Connection(string loc)
        {
            return conn = new SQLiteConnection($"Data Source={loc};Version=3");
        }

        public void Create_DB(string loc)
        {

            //Tworzenie bazy
            SQLiteConnection.CreateFile(loc);

            conn = new SQLiteConnection($"Data Source={loc};Version=3");
            conn.Open();

            //Tabela Urzytkownik
            sql = "CREATE TABLE User (UserID int primary key,NazwaUzytkownika varchar(50),Wzrost int, Plec varchar(20),ObecnaWaga decimal(4,2),DocelowaWaga decimal(4,2), KalorieNaDzien decimal(5,0), WodaNaDzien decimal(5,0), Wiek int) ";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            //Tabela Historia
            sql = "CREATE TABLE Historia (HistoriaID int primary key, UserID int, PosilekTyp varchar(30), DanyPosilek varchar(30), WagaPosilku int, KalorycznoscPosilku int, Woda int, Aktywnosc varchar(30), CzasAktywnosci varchar(30), Data text, foreign key(UserID) references User(UserID)) ";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();


            ////Pierwszy wpis testowy
            //sql = @"insert into User values(1,'Eustachy',178, 'h', 0.1, 0.2); ";
            //command = new SQLiteCommand(sql, conn);
            //command.ExecuteNonQuery();

            //sql = @"insert into Historia values(1,1,'Śniadanie', 's', 21, 37, 34, 'g', '55', '77'); ";
            //command = new SQLiteCommand(sql, conn);
            //command.ExecuteNonQuery();

            conn.Close();
        }


        public int GetUserID()
        {
            sql = "SELECT MAX(UserID) FROM User";
            command = new SQLiteCommand(sql, conn);
            int id = 0;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader[0] == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToInt32(reader[0]) + 1;
                return id;
            }
        }

        public void InsertUser(User s)
        {
            string obWaga= s.ObecnaWaga.ToString().Replace(',', '.');
            string docWaga = s.DocelowaWaga.ToString().Replace(',', '.');
            decimal KcalNaDzien = 0;
            int WodaNaDzien = 0;
            if (s.Plec == "kobieta")
            {
                KcalNaDzien = 655 + (9.6m * s.ObecnaWaga) + (1.85m * s.Wzrost) - (4.7m * s.Wiek);
                WodaNaDzien = 2000;
            }
            else
            {
                KcalNaDzien = 66.5m + (13.7m * s.ObecnaWaga) + (5m * s.Wzrost) - (6.8m * s.Wiek);
                WodaNaDzien = 2500;
            }

            if(s.ObecnaWaga > s.DocelowaWaga)
            {
                KcalNaDzien = KcalNaDzien * 0.8m;
            }
            if(s.ObecnaWaga < s.DocelowaWaga)
            {
                KcalNaDzien = KcalNaDzien * 1.2m;
            }


            sql = $"INSERT INTO User VALUES({s.UserID},'{s.NazwaUzytkownika}',{s.Wzrost},'{s.Plec}',{obWaga},{docWaga},{KcalNaDzien.ToString().Replace(',', '.')},{WodaNaDzien},{s.Wiek})";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public int GetHistoriaID()
        {
            sql = "SELECT MAX(HistoriaID) FROM Historia";
            command = new SQLiteCommand(sql, conn);
            int id = 0;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader[0] == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToInt32(reader[0]) + 1;
                return id;
            }
        }

        public bool InsertHistoria(Historia s)
        {
            string wagaPosilku = s.WagaPosilku.ToString().Replace(',', '.');
            string woda = s.Woda.ToString().Replace(',', '.');
            string czasAktywnosci = s.CzasAktywnosci.ToString().Replace(',', '.');
            string data = s.Data.ToShortDateString();
            sql = $"INSERT INTO Historia VALUES({s.HistoriaID},{s.UserID},'{s.PosilekTyp}','{s.DanyPosilek}',{wagaPosilku},{s.KalorycznoscPosilku},{woda},'{s.Aktywnosc}',{czasAktywnosci},'{data}')";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            return true;
        }

        public SQLiteDataAdapter ListaUzytkownikow()
        {
            SQLiteDataAdapter sda = new SQLiteDataAdapter("Select UserID,NazwaUzytkownika FROM User", conn);
            return sda;
        }

        public int WodaDzien()
        {
            sql = $"SELECT WodaNaDzien FROM User WHERE UserID = {ObecnyUzytkownik.wybrany_uzytkownik_id}";
            command = new SQLiteCommand(sql, conn);
            int id = 0;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader[0] == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToInt32(reader[0]);
                return id;
            }
        }

        public decimal KalorieDzien()
        {
            sql = $"SELECT KalorieNaDzien FROM User WHERE UserID = {ObecnyUzytkownik.wybrany_uzytkownik_id}";
            command = new SQLiteCommand(sql, conn);
            decimal id = 0;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader[0] == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToDecimal(reader[0]);
                return id;
            }
        }

        public int ZjedzoneKalorie(string Data)
        {
            sql = $"SELECT SUM(KalorycznoscPosilku) FROM Historia WHERE UserID = {ObecnyUzytkownik.wybrany_uzytkownik_id} AND Data = {Data}";
            command = new SQLiteCommand(sql, conn);
            int id = 0;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader[0] == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToInt32(reader[0]);
                return id;
            }
        }

        public int WypitaWoda(string Data)
        {
            sql = $"SELECT SUM(Woda) FROM Historia WHERE UserID = {ObecnyUzytkownik.wybrany_uzytkownik_id} AND Data = {Data}";
            command = new SQLiteCommand(sql, conn);
            int id = 0;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                if (reader[0] == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToInt32(reader[0]);
                return id;
            }
        }
    }
}
