namespace NineDigit.SpreadsheetLightExtensions
{
    public interface ITableFactory
    {
        CellReference CurrentCell { get; }
        
        /// <summary>
        /// Index of current table entry row (not the row of the current cell).
        /// </summary>
        int RowIndex { get; }

        /// <summary>
        /// Moves the <see cref="CurrentCell"/> to next cell and returns the vaule.
        /// </summary>
        /// <returns></returns>
        CellReference GetNextCell();
        /// <summary>
        /// Moves the <see cref="CurrentCell"/> to start of next row and returns the vaule.
        /// </summary>
        /// <returns></returns>
        CellReference GetNextRow();
    }
}
