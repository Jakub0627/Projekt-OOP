using System;
using System.Windows.Input;
using FlashcardApp.Services;

namespace FlashcardApp.ViewModels
{
    public class FlashcardViewModel
    {
        private readonly IUserProgressService _userProgressService;

        public FlashcardViewModel(IUserProgressService userProgressService)
        {
            _userProgressService = userProgressService;
            RecordAnswerCommand = new RelayCommand(RecordAnswerExecute);
        }

        public ICommand RecordAnswerCommand { get; private set; }

        private void RecordAnswerExecute(object parameter) //requires further attention!!!
        {
            if (parameter is int flashcardId)
            {
                try
                {
                    // Example logic - determine if the answer is correct
                    bool isCorrect = true; // This should be replaced with actual logic

                    // Record the user's answer
                    _userProgressService.RecordAnswer(flashcardId, isCorrect);
                }
                catch (Exception ex)
                {
                    // Handle exceptions - log or display error message
                }
            }
        }

        // Basic RelayCommand implementation
        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }
        }
    }
}
