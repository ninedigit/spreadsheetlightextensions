using System;
using System.Linq;

namespace NineDigit.SpreadsheetLightExtensions
{
    /// <summary>
    /// Represents column and row reference.
    /// </summary>
    public sealed class CellReference
    {
        public CellReference(ColumnReference column, RowReference row)
        {
            this.Column = column ?? throw new ArgumentNullException(nameof(column));
            this.Row = row ?? throw new ArgumentNullException(nameof(row));
        }

        public CellReference(string rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue) || rawValue.Any(c => !char.IsLetterOrDigit(c)))
                throw new ArgumentException("Invalid raw value. Only letters and digits allowed.", nameof(rawValue));

            var columnName = string.Join("", rawValue.TakeWhile(char.IsLetter));
            var rowNumber = int.Parse(string.Join("", rawValue.Skip(columnName.Length)));

            this.Column = columnName;
            this.Row = rowNumber;
        }

        /// <summary>
        /// First leftmost cell of sheet (A1).
        /// </summary>
        public readonly static CellReference TopLeft = new CellReference("A1");

        /// <summary>
        /// Column
        /// </summary>
        public ColumnReference Column { get; }
        /// <summary>
        /// Row
        /// </summary>
        public RowReference Row { get; }

        public static implicit operator string(CellReference d) => $"{d.Column}{d.Row}";
        public static implicit operator CellReference(string s) => new CellReference(s);

        public CellReference StartOfColumn()
            => new CellReference(this.Column, 1);

        public CellReference StartOfLine()
            => new CellReference(ColumnReference.First, this.Row);

        public CellReference AddLines(int linesCount)
            => new CellReference(this.Column, this.Row + linesCount);

        public CellReference NextLine()
            => this.AddLines(1);

        public CellReference PreviousLine()
            => this.AddLines(-1);

        public CellReference StartOfNextLine()
            => this.NextLine().StartOfLine();

        public CellReference PreviousColumn()
            => new CellReference(this.Column.GetPrevious(), this.Row);

        public CellReference NextColumn()
            => new CellReference(this.Column.GetNext(), this.Row);

        public override string ToString()
            => $"{this.Column}{this.Row}";
    }
}
