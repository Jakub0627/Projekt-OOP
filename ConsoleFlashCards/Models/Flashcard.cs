using ConsoleFlashCards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFlashCards.Models
{
    public class Flashcard : IFlashcard
    {
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

        public void DisplayFront()
        {
            Console.WriteLine(Front);
        }

        public void DisplayBack()
        {
            Console.WriteLine(Back);
        }
    }
}
