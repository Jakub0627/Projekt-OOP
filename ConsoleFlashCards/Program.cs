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
            string createSQL = "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)";
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = createSQL;
            sqliteCommand.ExecuteNonQuery();
        }

        // Umieszczanie danych
        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES ('Sample Text', 1);";
            sqliteCommand.ExecuteNonQuery();
        }

        // Czytanie danych
        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "SELECT * FROM SampleTable";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                string readerString = sqliteReader.GetString(0);
                Console.WriteLine(readerString);
            }
            conn.Close();
        }

        static void Main(string[] args)
        {
            // Test bazy
            SQLiteConnection sqliteConnection;
            sqliteConnection = CreateConnection();
            CreateTable(sqliteConnection);
            InsertData(sqliteConnection);
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
