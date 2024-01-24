using System;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;

namespace ConsoleFlashCards
{
    internal class Program
    {   


        static void Main(string[] args)
        {

            SQLiteManager sqliteManager = new SQLiteManager();
            SQLiteConnection sqliteConnection = sqliteManager.CreateConnection();
            sqliteManager.CreateTable(sqliteConnection);
            sqliteManager.InsertData(sqliteConnection, "frog", "żaba");
            sqliteManager.ReadData(sqliteConnection);
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
