using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Models
{
    public class Flashcard
    {
        // Properties
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        // Constructor
        public Flashcard(int id, string question, string answer)
        {
            Id = id;
            Question = question;
            Answer = answer;
        }

        // Methods for CRUD operations
        public static void CreateFlashcard(List<Flashcard> flashcards, Flashcard newFlashcard)
        {
            flashcards.Add(newFlashcard);
        }

        public static Flashcard ReadFlashcard(List<Flashcard> flashcards, int id)
        {
            return flashcards.FirstOrDefault(f => f.Id == id);
        }

        public static void UpdateFlashcard(Flashcard flashcard, string question, string answer)
        {
            flashcard.Question = question;
            flashcard.Answer = answer;
        }

        public static void DeleteFlashcard(List<Flashcard> flashcards, Flashcard flashcard)
        {
            flashcards.Remove(flashcard);
        }
    }

}
