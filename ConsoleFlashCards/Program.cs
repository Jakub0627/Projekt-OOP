using ConsoleFlashCards.Interfaces;
using ConsoleFlashCards.Models;
using System;
using System.Data.SQLite;
using System.Threading;

namespace ConsoleFlashCards
{
    internal class Program
    {
        private readonly ISQLiteManager sqliteManager;

        public Program(ISQLiteManager sqliteManager)
        {
            this.sqliteManager = sqliteManager;
        }

        public void Run()
        {
            SQLiteConnection sqliteConnection = sqliteManager.CreateConnection();

            Console.WriteLine("APLIKACJA DO FISZEK");
            Console.WriteLine("Instrukcja: Podaj daną liczbę aby:");
            Console.WriteLine("1 - Nauka");
            Console.WriteLine("2 - Obecny wynik");
            Console.WriteLine("3 - Pokaż bazę słówek");
            Console.WriteLine("4 - Utwórz nową fiszkę");
            Console.WriteLine("5 - Edytuj fiszkę");
            Console.WriteLine("6 - Usuń fiszkę");
            Console.WriteLine("7 - Koniec nauki.");
            Console.WriteLine("\n Po pokazaniu się fiszki masz 5s na zastanowienie się czy znasz odpowiedź");
            Console.WriteLine("Jeśli znałeś odpowiedź, użyj 'y', jeśli nie, użyj 'n', program sam policzy punkty");

            ScoreCounter scoreCounter = new ScoreCounter();
            bool isRunning = true;
            while (isRunning)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Nauka: użyj 'y' jeśli znasz odpowiedź, jeśli nie użyj 'n'. Użycie dowolnej innej opcji zatwierdzi powrót do menu.");
                        do
                        {
                            Flashcard flashcard = sqliteManager.GetRandomFlashcard(sqliteConnection);
                            Console.WriteLine($"Jak jest po polsku?: {flashcard.Back}");

                            Thread.Sleep(5000);
                            Console.WriteLine($"Odpowiedź to: {flashcard.Front}");
                            input = Console.ReadLine();

                            if (input == "y") scoreCounter.Good();
                            else if (input == "n") scoreCounter.Bad();

                        } while (input == "n" || input == "y");
                        break;

                    case "2":
                        scoreCounter.ShowScore();
                        break;

                    case "3":
                        sqliteManager.ReadData(sqliteConnection);
                        break;

                    case "4":
                        Console.WriteLine("Tworzenie nowej fiszki.");
                        Console.Write("Podaj treść po angielsku (Front): ");
                        string front = Console.ReadLine();

                        Console.Write("Podaj tłumaczenie po polsku (Back): ");
                        string back = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(front) && !string.IsNullOrWhiteSpace(back))
                        {
                            sqliteManager.InsertData(sqliteConnection, front, back);
                            Console.WriteLine("Nowa fiszka została dodana do bazy.");
                        }
                        else
                        {
                            Console.WriteLine("Nie wprowadzono odpowiednich danych. Fiszka nie została utworzona.");
                        }
                        break;

                    case "5":
                        Console.Write("Podaj ID fiszki do edycji: ");
                        if (int.TryParse(Console.ReadLine(), out int editId))
                        {
                            Console.Write("Nowa treść po angielsku (Front): ");
                            string newFront = Console.ReadLine();

                            Console.Write("Nowe tłumaczenie po polsku (Back): ");
                            string newBack = Console.ReadLine();

                            sqliteManager.UpdateData(sqliteConnection, editId, newFront, newBack);
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowe ID.");
                        }
                        break;

                    case "6":
                        Console.Write("Podaj ID fiszki do usunięcia: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            sqliteManager.DeleteData(sqliteConnection, deleteId);
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowe ID.");
                        }
                        break;

                    case "7":
                        isRunning = false;
                        sqliteManager.CloseConnection(sqliteConnection);
                        Console.WriteLine("Koniec nauki. Zamykanie aplikacji...");
                        break;

                    default:
                        Console.WriteLine("Nieznana opcja, spróbuj ponownie.");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            ISQLiteManager sqliteManager = new SQLiteManager();
            Program program = new Program(sqliteManager);
            program.Run();
        }
    }
}
