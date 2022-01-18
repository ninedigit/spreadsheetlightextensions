# NineDigit Spread Sheet Light Extensions

[![NuGet version (NineDigit.SpreadSheetLightExtensions)](https://img.shields.io/nuget/v/NineDigit.SpreadSheetLightExtensions)](https://www.nuget.org/packages/NineDigit.SpreadSheetLightExtensions/)

Extensions for the [spreadsheetlight library](http://spreadsheetlight.com/).

## Fluent cell referencing

Type safe cell referencing (column name and row index)

```csharp
var cell = CellReference.TopLeft // "A1"
    .AddLines(4) // "A5"
    .NextColumn() // "B5"
    .StartOfNextLine() // "A6"
    .NextColumn() // "B6"
    .StartOfLine() // "A6"
    .NextLine() // "A7"
    .NextColumn() // "B7"
    .StartOfColumn(); // "A7"
```

## Table helper

```csharp
// top-left cell of the thable
var topLeftCell = CellReference.TopLeft;

var tableColumnHeaders = new string[]
{
    "Foo",
    "Bar",
    "Baz",
};

// create the table factory
var table = doc.CreateTableFactory(topLeftCell, tableColumnHeaders);

// first cell of the first row - table.CurrentCell
doc.SetCellValue(table.CurrentCell, "foo1");
// other cells in same row - table.GetNextCell()
doc.SetNumericCellValue(table.GetNextCell(), 1.234M);
doc.SetNumericCellValue(table.GetNextCell(), 99.99M, "0.00%");

// move to next row
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
```
