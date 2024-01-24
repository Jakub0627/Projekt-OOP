using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SQLite;

namespace ConsoleFlashCards
{
    public class SQLiteManager : ISQLiteManager
    {
        public SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqliteConn;
            sqliteConn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True;");
            try
            {
                sqliteConn.Open();
            }
            catch
            {

            }
            return sqliteConn;
        }

        public void CreateTable(SQLiteConnection conn)
        {
            // Sprawdzenie, czy tabela już istnieje
            SQLiteCommand checkTableCommand = conn.CreateCommand();
            checkTableCommand.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='FlashCards';";
            var result = checkTableCommand.ExecuteScalar();

            if (result != null && result.ToString() == "FlashCards")
            {
                Console.WriteLine("Tabela 'FlashCards' już istnieje.");
                return;
            }

            // Utworzenie tabeli, jeśli nie istnieje
            SQLiteCommand sqliteCommand;
            string createSQL = "CREATE TABLE FlashCards(id INTEGER PRIMARY KEY AUTOINCREMENT, Front VARCHAR(100), Back VARCHAR(100))";
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = createSQL;
            sqliteCommand.ExecuteNonQuery();

            Console.WriteLine("Tabela 'FlashCards' została utworzona.");
        }

        public void InsertData(SQLiteConnection conn, string front, string back)
        {
            // Sprawdzenie czy rekord już istnieje
            SQLiteCommand checkCommand = conn.CreateCommand();
            checkCommand.CommandText = "SELECT COUNT(*) FROM FlashCards WHERE Front = @front OR Back = @back";
            checkCommand.Parameters.AddWithValue("@front", front);
            checkCommand.Parameters.AddWithValue("@back", back);

            int count = Convert.ToInt32(checkCommand.ExecuteScalar());
            if (count > 0)
            {
                Console.WriteLine("Rekord z takim samym Front lub Back już istnieje.");
                return;
            }

            // Dodanie nowego rekordu, jeśli nie istnieje
            SQLiteCommand insertCommand = conn.CreateCommand();
            insertCommand.CommandText = "INSERT INTO FlashCards(Front, Back) VALUES (@front, @back);";
            insertCommand.Parameters.AddWithValue("@front", front);
            insertCommand.Parameters.AddWithValue("@back", back);
            insertCommand.ExecuteNonQuery();
        }

        public void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "SELECT * FROM FlashCards";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                int id = sqliteReader.GetInt32(0);
                string front = sqliteReader.GetString(1);
                string back = sqliteReader.GetString(2);

                Console.WriteLine($"ID: {id}, Front: {front}, Back: {back}");
            }
            conn.Close();
        }
    }
}