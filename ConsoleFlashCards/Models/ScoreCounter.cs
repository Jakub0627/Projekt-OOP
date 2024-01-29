using ConsoleFlashCards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFlashCards.Models
{
    public class ScoreCounter : IScoreCounter
    {
        private int goodAnswers;
        private int badAnswers;

        public ScoreCounter()
        {
            goodAnswers = 0;
            badAnswers = 0;
        }

        public void Good()
        {
            goodAnswers++;
            Console.WriteLine("Poprawna odpowiedź!");
        }

        public void Bad()
        {
            badAnswers++;
            Console.WriteLine("Niepoprawna odpowiedź.");
        }

        public void ShowScore()
        {
            int allAnswers = goodAnswers + badAnswers;
            Console.WriteLine($"Wynik: {goodAnswers}/{allAnswers} poprawnych odpowiedzi.");
        }

        public void Reset()
        {
            goodAnswers = 0;
            badAnswers = 0;
            Console.WriteLine("Statystyki zostały zresetowane.");
        }
    }
}
