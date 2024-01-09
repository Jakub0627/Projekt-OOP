using FlashcardApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp
{
    public class DatabaseHelper
    {
        private static string connectionString = "Data Source=flashcards.db";

        // Method to create the database file
        public static void CreateDatabase()
        {
            string dbFileName = "flashcards.db";
            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }
        }

        // Method to create the table
        public static void CreateTable()
        {
            string connectionString = "Data Source=flashcards.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS Flashcards (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Question TEXT NOT NULL,
                                Answer TEXT NOT NULL)";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Insert a new flashcard
        public static void InsertFlashcard(Flashcard flashcard)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Flashcards (Question, Answer) VALUES (@Question, @Answer)";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Question", flashcard.Question);
                    command.Parameters.AddWithValue("@Answer", flashcard.Answer);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Retrieve all flashcards
        public static List<Flashcard> GetAllFlashcards()
        {
            List<Flashcard> flashcards = new List<Flashcard>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Flashcards";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flashcard flashcard = new Flashcard
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Question = reader["Question"].ToString(),
                                Answer = reader["Answer"].ToString()
                            };
                            flashcards.Add(flashcard);
                        }
                    }
                }
            }
            return flashcards;
        }

        // Update an existing flashcard
        public static void UpdateFlashcard(Flashcard flashcard)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Flashcards SET Question = @Question, Answer = @Answer WHERE Id = @Id";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", flashcard.Id);
                    command.Parameters.AddWithValue("@Question", flashcard.Question);
                    command.Parameters.AddWithValue("@Answer", flashcard.Answer);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a flashcard
        public static void DeleteFlashcard(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Flashcards WHERE Id = @Id";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
