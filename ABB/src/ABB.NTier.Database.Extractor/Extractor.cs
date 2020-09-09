using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClosedXML.Excel;

namespace ABB.NTier.Database.Etl
{
    public static class Extractor
    {
        public static DataTable GetInitialData(string filePath, string mainColumn, string[] columns, int sheetNumber)
        {
            DataTable dataTable;

            using (var workBook = new XLWorkbook(filePath))
            {
                var workSheet = workBook.Worksheet(sheetNumber);
                var firstRowUsed = workSheet.FirstRowUsed();
                var firstPossibleAddress = workSheet.Row(firstRowUsed.RowNumber()).FirstCell().Address;
                var lastPossibleAddress = workSheet.LastCellUsed().Address;

                // Get a range with the remainder of the worksheet data (the range used)
                var range = workSheet.Range(firstPossibleAddress, lastPossibleAddress).AsRange(); //.RangeUsed();
                                                                                                  // Treat the range as a table (to be able to use the column names)
                var table = range.AsTable();

                //Specify what are all the Columns you need to get from Excel
                var dataList = new List<string[]>();
                dataList.Add(table.DataRange.Rows().Select(tableRow => tableRow.Field(mainColumn).GetString()).ToArray());

                foreach (var column in columns)
                {
                    dataList.Add(table.DataRange.Rows().Select(tableRow => tableRow.Field(column).GetString()).ToArray());
                }

                //Convert List to DataTable
                dataTable = ConvertListToDataTable(dataList, mainColumn, columns);

                Console.WriteLine("Total number of unique solution number in Excel : " + dataTable.Rows.Count);
            }

            return dataTable;
        }

        private static DataTable ConvertListToDataTable(IReadOnlyList<string[]> list, string mainColumn, IReadOnlyList<string> columns)
        {
            var table = new DataTable("CustomTable");
            var rows = list.Select(array => array.Length).Concat(new[] { 0 }).Max();
            
            table.Columns.Add(mainColumn);

            foreach (var column in columns)
            {
                table.Columns.Add(column);
            }
            
            for (var j = 0; j < rows; j++)
            {
                var row = table.NewRow();
                row[mainColumn] = list[0][j];

                for (var index = 0; index < columns.Count; index++)
                {
                    var column = columns[index];
                    row[column] = list[index + 1][j];
                }

                table.Rows.Add(row);
            }
            return table;
        }
    }
}
