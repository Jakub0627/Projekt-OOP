using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Services
{
    public class UserProgressService : IUserProgressService
    {
        private UserProgress _currentProgress;

        public UserProgressService()
        {
            _currentProgress = new UserProgress();
        }

        public void RecordAnswer(int flashcardId, bool isCorrect)
        {
            // Increment the total attempts
            _currentProgress.TotalAttempts++;

            // Increment the correct answers count if the answer is correct
            if (isCorrect)
            {
                _currentProgress.CorrectAnswers++;
            }
        }

        public UserProgress GetProgress()
        {
            return new UserProgress
            {
                TotalAttempts = _currentProgress.TotalAttempts,
                CorrectAnswers = _currentProgress.CorrectAnswers
            };
        }
    }
}