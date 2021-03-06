﻿using System;
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
            
            // Set the cell based one whose turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Circle;
            
            // Set button text to result
            button.Content = mPlayer1Turn ? "X" : "O";
            
            // Change circles to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Green;

            // Toggle the players turn
            mPlayer1Turn ^= true;
            
            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            
            // Check for horizontal line wins
            // 
            // Row - 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            // 
            // Row - 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            // 
            // Row - 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            
            #endregion
            
            #region Vertical Wins
            
            // Check for vertical line wins
            // 
            // Column - 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            // 
            // Column - 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            // 
            // Column - 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            
            #endregion
            
            #region Diagonal Winners
            
            // Check for diagonal line wins
            // 
            // Top Left Bottom Right
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            // 
            // Top Right Bottom Left
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // End game
                mGameEnded = true;
                
                // Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            
            #endregion
            
            #region No Winners
            
            // Check for no winner and full board
            if (!mResults.Any(f => f == MarkType.Free))
            {
                // End game
                mGameEnded = true;
                
                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}