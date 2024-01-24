using System;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;

namespace ConsoleFlashCards
{
    internal class Program
    {   
        // Część odpowiedzialna za bazy danych :))
        static SQLiteConnection CreateConnection()
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

        // Tworzenie tabel
        static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqliteCommand;
            string createSQL = "CREATE TABLE FlashCards(id INTEGER PRIMARY KEY AUTOINCREMENT, Front VARCHAR(100), Back VARCHAR(100))";
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = createSQL;
            sqliteCommand.ExecuteNonQuery();
        }

        // Umieszczanie danych
        static void InsertData(SQLiteConnection conn, string front, string back)
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

        // Czytanie danych
        static void ReadData(SQLiteConnection conn)
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


        static void Main(string[] args)
        {
            // Test bazy
            SQLiteConnection sqliteConnection;
            sqliteConnection = CreateConnection();
            //CreateTable(sqliteConnection);
            InsertData(sqliteConnection, "cat", "kot");
            ReadData(sqliteConnection);
            // koniec testu

            Console.WriteLine("APLIKACJA DO FISZEK");
            Console.WriteLine("Instrukcja: Podaj daną liczbę aby: ");
            Console.WriteLine("1 - Start nauki");
            Console.WriteLine("2 - Koniec nauki");
            Console.WriteLine("3 - obecny wynik");

            bool isRunning = true;
            while (isRunning)
            {
                string input = Console.ReadLine();
                int choice;

                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            // Logika rozpoczynająca naukę
                            //StartLearning();
                            Console.WriteLine("start learning");
                            break;
                        case 2:
                            // Zakończenie programu
                            Console.WriteLine("Zakończenie nauki.");
                            isRunning = false;
                            break;
                        case 3:
                            // Wyświetl obecny wynik
                            //ShowCurrentResult();
                            Console.WriteLine("Obecny wynik");
                            break;
                        default:
                            Console.WriteLine("Nieznana opcja, spróbuj ponownie.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Proszę podać liczbę.");
                }
            }
        }
    }
}
