using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClosedXML.Excel;

namespace ABB.NTier.Database.Etl
{
    public class Extractor
    {
        public DataTable GetInitialData(string filePath)
        {
            DataTable dataTable;

            using (var workBook = new XLWorkbook(filePath))
            {
                var workSheet = workBook.Worksheet(0);
                var firstRowUsed = workSheet.FirstRowUsed();
                var firstPossibleAddress = workSheet.Row(firstRowUsed.RowNumber()).FirstCell().Address;
                var lastPossibleAddress = workSheet.LastCellUsed().Address;

                // Get a range with the remainder of the worksheet data (the range used)
                var range = workSheet.Range(firstPossibleAddress, lastPossibleAddress).AsRange(); //.RangeUsed();
                                                                                                  // Treat the range as a table (to be able to use the column names)
                var table = range.AsTable();

                //Specify what are all the Columns you need to get from Excel
                var dataList = new List<string[]>
                {
                    table.DataRange.Rows().Select(tableRow => tableRow.Field("Solution Number").GetString()).ToArray(),
                    table.DataRange.Rows().Select(tableRow => tableRow.Field("Name").GetString()).ToArray(),
                    table.DataRange.Rows().Select(tableRow => tableRow.Field("Date").GetString()).ToArray()
                };

                //Convert List to DataTable
                dataTable = ConvertListToDataTable(dataList);

                //To get unique column values, to avoid duplication
                var uniqueCols = dataTable.DefaultView.ToTable(true, "Solution Number");

                //Remove Empty Rows or any specify rows as per your requirement
                for (var i = uniqueCols.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = uniqueCols.Rows[i];
                    if (dr != null && ((string)dr["Solution Number"] == "None" || (string)dr["Title"] == ""))
                        dr.Delete();
                }

                Console.WriteLine("Total number of unique solution number in Excel : " + uniqueCols.Rows.Count);
            }

            return dataTable;
        }

        private DataTable ConvertListToDataTable(IReadOnlyList<string[]> list)
        {
            var table = new DataTable("CustomTable");
            var rows = list.Select(array => array.Length).Concat(new[] { 0 }).Max();

            table.Columns.Add("Solution Number");
            table.Columns.Add("Name");
            table.Columns.Add("Date");

            for (var j = 0; j < rows; j++)
            {
                var row = table.NewRow();
                row["Solution Number"] = list[0][j];
                row["Name"] = list[1][j];
                row["Date"] = list[2][j];
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
