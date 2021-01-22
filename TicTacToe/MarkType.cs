namespace TicTacToe
{
    /// <summary>
    /// The current symbol in an area 
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// The cell hasn't been clicked yet
        /// </summary>
        Free,
        /// <summary>
        /// The cell has an O in it
        /// </summary>
        Circle,
        /// <summary>
        /// The cell has an X in it
        /// </summary>
        Cross
    }
}