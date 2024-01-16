using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFlashCards.Interfaces
{
    public interface IFlashcard
    {
        int Id { get; set; }
        string Front { get; set; }
        string Back { get; set; }

        void DisplayFront();
        void DisplayBack();
    }
}

