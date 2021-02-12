using System;

namespace NineDigit.SpreadsheetLightExtensions
{
    internal class TableFactory : ITableFactory
    {
        int currentColumnIndex;

        readonly int columnsCount;
        readonly CellReference firstColumnCell;

        public TableFactory(int columnsCount, CellReference firstColumnCell)
        {
            this.columnsCount = columnsCount;
            this.firstColumnCell = this.CurrentCell = firstColumnCell
                ?? throw new ArgumentNullException(nameof(firstColumnCell));
        }

        public int RowIndex { get; private set; }
        public CellReference CurrentCell { get; private set; }

        public CellReference GetNextCell()
        {
            if (this.currentColumnIndex == this.columnsCount)
                throw new InvalidOperationException("Attepmt to insert cell in column that exceeds maximal columns count.");

            this.currentColumnIndex++;
            return this.CurrentCell = this.CurrentCell.NextColumn();
        }

        public CellReference GetNextRow()
        {
            this.RowIndex++;
            this.currentColumnIndex = 0;
            return this.CurrentCell = this.firstColumnCell.AddLines(this.RowIndex);
        }
    }
}
