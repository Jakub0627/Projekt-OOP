using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Services
{
    public interface IUserProgressService
    {
        void RecordAnswer(int flashcardId, bool isCorrect);
        UserProgress GetProgress();
    }

    public class UserProgress
    {
        public int TotalAttempts { get; set; }
        public int CorrectAnswers { get; set; }
    }
}
