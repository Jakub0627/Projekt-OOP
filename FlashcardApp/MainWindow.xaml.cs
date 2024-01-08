using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlashcardApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Initialize flashcard content here if needed
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to show the answer
            DidntKnowButton.Visibility = Visibility.Visible;
            KnowButton.Visibility = Visibility.Visible;
        }

        private void DidntKnowButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for handling "Didn't Know" response
        }

        private void KnowButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for handling "Know" response
        }

        // Other methods and event handlers
    }
}