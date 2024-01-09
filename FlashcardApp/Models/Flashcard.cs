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
        public Flashcard() { }

        public Flashcard(int id, string question, string answer)
        {
            Id = id;
            Question = question;
            Answer = answer;
        }
    }
}
