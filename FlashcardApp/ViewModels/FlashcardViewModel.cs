using System;
using System.Windows.Input;
using FlashcardApp.Services;
using System.Windows; // Required for MessageBox

namespace FlashcardApp.ViewModels
{
    public class FlashcardViewModel
    {
        private readonly IUserProgressService _userProgressService;

        // Commands for different functionalities
        public ICommand StartLearningCommand { get; private set; }
        public ICommand CreateFlashcardsCommand { get; private set; }
        public ICommand ReviewProgressCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand RecordAnswerCommand { get; private set; }

        public FlashcardViewModel(IUserProgressService userProgressService)
        {
            _userProgressService = userProgressService;

            // Initialize commands
            StartLearningCommand = new RelayCommand(_ => StartLearning());
            CreateFlashcardsCommand = new RelayCommand(_ => CreateFlashcards());
            ReviewProgressCommand = new RelayCommand(_ => ReviewProgress());
            SettingsCommand = new RelayCommand(_ => OpenSettings());
            RecordAnswerCommand = new RelayCommand(RecordAnswerExecute);
        }

        private void StartLearning()
        {
            MessageBox.Show("Start Learning clicked");
        }

        private void CreateFlashcards()
        {
            MessageBox.Show("Create Flashcards clicked");
        }

        private void ReviewProgress()
        {
            MessageBox.Show("Review Progress clicked");
        }

        private void OpenSettings()
        {
            MessageBox.Show("Settings clicked");
        }

        private void RecordAnswerExecute(object parameter)
        {
            if (parameter is bool isCorrect)
            {
                try
                {
                    // Example logic
                    // Record the user's answer
                    _userProgressService.RecordAnswer(0, isCorrect); // 0 is a placeholder
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }
            }
        }

        // RelayCommand implementation
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
