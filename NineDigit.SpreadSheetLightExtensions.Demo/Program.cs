using SpreadsheetLight;

namespace NineDigit.SpreadsheetLightExtensions.Demo
{
    public class Program
    {
        public void Main(string[] args)
        { 
            var doc = new SLDocument();
            doc.DocumentProperties.Title = "Demo";

            var cell = CellReference.TopLeft
                .AddLines(10)
                .NextColumn()
                .StartOfNextLine()
                .NextColumn()
                .StartOfLine()
                .NextLine()
                .NextColumn()
                .StartOfColumn();

            // top-left cell of the thable
            var topLeftCell = CellReference.TopLeft;

            var tableColumnHeaders = new string[]
            {
                "Foo",
                "Bar",
                "Baz",
            };

            var table = doc.CreateTableFactory(topLeftCell, tableColumnHeaders);

            doc.SetCellValue(table.CurrentCell, "foo1");
            doc.SetNumericCellValue(table.GetNextCell(), 1.234M);
            doc.SetNumericCellValue(table.GetNextCell(), 99.99M, "0.00%");

            doc.SetCellValue(table.GetNextRow(), "foo2");
            doc.SetNumericCellValue(table.GetNextCell(), 1.234M);
            doc.SetNumericCellValue(table.GetNextCell(), 99.99M, "0.00%");

            // right-bottom cell of the table
            var rightBottomCell = table.CurrentCell;

            var docTable = doc.CreateTable(topLeftCell, rightBottomCell);

            docTable.HasTotalRow = true;
            docTable.SetTotalRowLabel(1, "Total");
            docTable.SetTotalRowFunction(2, SLTotalsRowFunctionValues.Sum);

            doc.InsertTable(docTable);

            cell = rightBottomCell.NextLine(); // add one line, due to total row

            doc.SetCellValue(cell, "End of document");

            doc.SaveAs("file-name.xlsx");
        }
    }
}
