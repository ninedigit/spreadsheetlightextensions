using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;

namespace NineDigit.SpreadsheetLightExtensions
{
    public static class SLDocumentExtensions
    {
        public static ITableFactory CreateTableFactory(this SLDocument doc, CellReference topLeftCell, IReadOnlyCollection<string> columnHeaders)
        {
            if (doc is null)
                throw new ArgumentNullException(nameof(doc));
            if (topLeftCell is null)
                throw new ArgumentNullException(nameof(topLeftCell));
            if (columnHeaders is null)
                throw new ArgumentNullException(nameof(columnHeaders));
            if (columnHeaders.Count == 0)
                throw new ArgumentException("At least one column expected.", nameof(columnHeaders));

            var headerCell = topLeftCell;

            foreach (var columnHeaderText in columnHeaders)
            {
                doc.SetCellValue(headerCell, columnHeaderText);
                headerCell = headerCell.NextColumn();
            }

            doc.SetCellStyle(topLeftCell, headerCell, new SLStyle()
            {
                Font = new SLFont()
                {
                    Bold = true,
                }
            });

            return new TableFactory(columnHeaders.Count, topLeftCell.NextLine());
        }

        public static void SetNumericCellValue(this SLDocument self, CellReference cell, decimal value, string format = "#,##0.00")
        {
            if (self is null)
                throw new ArgumentNullException(nameof(self));

            if (cell is null)
                throw new ArgumentNullException(nameof(cell));

            if (string.IsNullOrEmpty(format))
                throw new ArgumentException($"'{nameof(format)}' cannot be null or empty", nameof(format));

            self.SetCellValue(cell, value);
            var style = self.GetCellStyle(cell);
            style.FormatCode = format;
            self.SetCellStyle(cell, style);
        }

        public static bool SetCellBackground(this SLDocument doc, CellReference cellReference, System.Drawing.Color color)
        {
            if (doc is null)
                throw new ArgumentNullException(nameof(doc));

            if (cellReference is null)
                throw new ArgumentNullException(nameof(cellReference));

            var style = doc.GetCellStyle(cellReference);
            style.Fill.SetPattern(PatternValues.Solid, ForegroundColor: color, BackgroundColor: color);
            return doc.SetCellStyle(cellReference, style);
        }

        public static bool SetCellFontSize(this SLDocument doc, CellReference cellReference, int fontSize)
        {
            if (doc is null)
                throw new ArgumentNullException(nameof(doc));

            if (cellReference is null)
                throw new ArgumentNullException(nameof(cellReference));

            var style = doc.GetCellStyle(cellReference);
            style.Font.FontSize = fontSize;
            return doc.SetCellStyle(cellReference, style);
        }

        public static bool SetCellFontColor(this SLDocument doc, CellReference cellReference, System.Drawing.Color fontColor)
        {
            if (doc is null)
                throw new ArgumentNullException(nameof(doc));

            if (cellReference is null)
                throw new ArgumentNullException(nameof(cellReference));

            var style = doc.GetCellStyle(cellReference);
            style.Font.FontColor = fontColor;
            return doc.SetCellStyle(cellReference, style);
        }

        public static bool SetCellBold(this SLDocument doc, CellReference cellReference, bool bold = true)
        {
            if (doc is null)
                throw new ArgumentNullException(nameof(doc));

            if (cellReference is null)
                throw new ArgumentNullException(nameof(cellReference));

            var style = doc.GetCellStyle(cellReference);
            style.Font.Bold = bold;
            return doc.SetCellStyle(cellReference, style);
        }
    }
}
