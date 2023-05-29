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
            sql = "CREATE TABLE User (UserID int primary key,NazwaUzytkownika varchar(50),Wzrost int, Plec varchar(20),ObecnaWaga decimal(4,2),DocelowaWaga decimal(4,2)) ";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            //Tabela Historia
            sql = "CREATE TABLE Historia (HistoriaID int primary key, UserID int, PosilekTyp varchar(30), DanyPosilek varchar(30), WagaPosilku int, KalorycznoscPosilku int, Woda int, Aktywnosc varchar(30), CzasAktywnosci varchar(30), Data text, foreign key(UserID) references User(UserID)) ";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();


            //Pierwszy wpis testowy
            sql = @"insert into User values(1,'Eustachy',178, 'h', 0.1, 0.2); ";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = @"insert into Historia values(1,1,'Śniadanie', 's', 21, 37, 'g', '55', '77'); ";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }

    }
}
