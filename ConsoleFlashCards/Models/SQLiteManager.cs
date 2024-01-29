using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SQLite;
using ConsoleFlashCards.Models;

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
            // Sprawdzenie połączenia
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

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
            // Sprawdzenie połączenia
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

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
            // Sprawdzenie połączenia
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            SQLiteCommand sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "SELECT * FROM FlashCards";

            using (SQLiteDataReader reader = sqliteCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string front = reader.GetString(1);
                    string back = reader.GetString(2);

                    Console.WriteLine($"ID: {id}, Front: {front}, Back: {back}");
                }
            }

        }

        public Flashcard? GetRandomFlashcard(SQLiteConnection conn)
        {
            // Sprawdzenie połączenia
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            SQLiteCommand sqliteCommand = conn.CreateCommand();

            // Zapytanie o losową fiszkę
            sqliteCommand.CommandText = "SELECT id, Front, Back FROM FlashCards ORDER BY RANDOM() LIMIT 1";

            using (SQLiteDataReader reader = sqliteCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string front = reader.GetString(1);
                    string back = reader.GetString(2);

                    return new Flashcard(id, front, back);
                }
            }
            return null;
          
        }

        public void UpdateData(SQLiteConnection conn, int id, string front, string back)
        {
            // Sprawdzenie połączenia
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE FlashCards SET Front = @front, Back = @back WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@front", front);
                cmd.Parameters.AddWithValue("@back", back);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Fiszka została zaktualizowana.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono fiszki o podanym ID.");
                }
            }
        }

        public void DeleteData(SQLiteConnection conn, int id)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM FlashCards WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Fiszka o ID {id} została usunięta.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono fiszki o podanym ID.");
                }
            }
        }

        public void CloseConnection(SQLiteConnection conn)
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                Console.WriteLine("Połączenie z bazą danych zostało zamknięte.");
            }
        }
    }
}