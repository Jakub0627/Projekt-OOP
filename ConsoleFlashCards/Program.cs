using ConsoleFlashCards.Interfaces;
using ConsoleFlashCards.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Threading;

namespace ConsoleFlashCards
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            SQLiteManager sqliteManager = new SQLiteManager();
            SQLiteConnection sqliteConnection = sqliteManager.CreateConnection();

            // Logika aplikacji:
            Console.WriteLine("APLIKACJA DO FISZEK");
            Console.WriteLine("Instrukcja: Podaj daną liczbę aby: ");
            Console.WriteLine("1 - Nauka");
            Console.WriteLine("2 - Obecny wynik");
            Console.WriteLine("3 - Pokaż bazę słówek");
            Console.WriteLine("4 - Utwórz nową fiszkę");
            Console.WriteLine("5 - Edytuj fiszkę");
            Console.WriteLine("6 - Usuń fiszkę");
            Console.WriteLine("7 - Koniec nauki.");
            Console.WriteLine("y/n - wiem/nie wiem, podczas nauki");
            Console.WriteLine();

            ScoreCounter scoreCounter = new ScoreCounter();
            bool isRunning = true;
            while (isRunning)
            {
                string input = Console.ReadLine();

                if (input == "1")
                {
                    // start nauki
                    Console.WriteLine("Nauka: użyj 'y' jeśli znasz odpowiedź, jeśli nie użyj 'n'. Użycie dowolnej innej opcji, zatwierdzi powrót do wyboru opcji 1-6.");

                    do
                    {                       
                        Flashcard flashcard = sqliteManager.GetRandomFlashcard(sqliteConnection);

                        Console.WriteLine($"Jak jest po polsku?: {flashcard.Back}");
                        
                        //czekaj 5s przed pokazaniem odpowiedzi
                        Thread.Sleep(5000);

                        Console.WriteLine($"Odpowiedź to: {flashcard.Front}");
                        input = Console.ReadLine();

                        if (input == "y")
                        {                         
                            scoreCounter.Good();
                        }
                        else if (input == "n")
                        {
                            scoreCounter.Bad();
                        }

                    } while (input == "n" ||  input == "y");

                }
                else if (input == "2") 
                {
                    scoreCounter.ShowScore();

                }
                else if (input == "3")
                {
                    sqliteManager.ReadData(sqliteConnection);
                }
                else if (input == "4")
                {
                    Console.WriteLine("Tworzenie nowej fiszki.");
                    Console.Write("Podaj treść po angielsku (Front): ");
                    string front = Console.ReadLine();

                    Console.Write("Podaj tłumaczenie po polsku (Back): ");
                    string back = Console.ReadLine();

                    // walidacja
                    if (!string.IsNullOrWhiteSpace(front) && !string.IsNullOrWhiteSpace(back))
                    {
                        sqliteManager.InsertData(sqliteConnection, front, back);
                        Console.WriteLine("Nowa fiszka została dodana do bazy.");
                    }
                    else
                    {
                        Console.WriteLine("Nie wprowadzono odpowiednich danych. Fiszka nie została utworzona.");
                    }
                }
                else if (input == "5")
                {
                    Console.Write("Podaj ID fiszki do edycji: ");
                    int id;
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("Nowa treść po angielsku (Front): ");
                        string front = Console.ReadLine();

                        Console.Write("Nowe tłumaczenie po polsku (Back): ");
                        string back = Console.ReadLine();

                        sqliteManager.UpdateData(sqliteConnection, id, front, back);
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowe ID.");
                    }
                }
                else if (input == "6")
                {
                    Console.Write("Podaj ID fiszki do usunięcia: ");
                    int id;
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        sqliteManager.DeleteData(sqliteConnection, id);
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowe ID.");
                    }
                }
                else if (input == "7")
                {
                    isRunning = false;
                    sqliteManager.CloseConnection(sqliteConnection);
                    Console.WriteLine("Koniec nauki. Zamykanie aplikacji...");
                }
                else
                {
                    Console.WriteLine("Nieznana opcja, spróbuj ponownie.");
                }
            }
        }
    }
}
