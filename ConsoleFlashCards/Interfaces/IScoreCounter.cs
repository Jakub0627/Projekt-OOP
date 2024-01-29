using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFlashCards.Interfaces
{
    public interface IScoreCounter
    {
        void Good();
        void Bad();
        void ShowScore();
        void Reset();
    }
}
