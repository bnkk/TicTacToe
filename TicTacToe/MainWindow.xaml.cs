using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds current results of cells in an active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is Player 1's turn (X) or Player 2's turn (X)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game is ended
        /// </summary>
        private bool mGameEnded;
        
        #endregion
        
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion
        
        /// <summary>
        /// Starts new game and resets all values
        /// </summary>
        private void NewGame()
        {
            // Create blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;
            
            // Make sure Player 1 starts the game
            mPlayer1Turn = true;

            // Interact with every button on the grid...
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground, and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            
            // Make sure game hasn't ended
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button was clicked</param>
        /// <param name="e">The events of said button</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start new game on click after finish
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            
            // Cast the sender to a button
            var button = (Button)sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            
            var index = column + (row * 3);

            // Don't do anything if the cell isn't free
            if (mResults[index] != MarkType.Free)
                return;
        }
    }
}