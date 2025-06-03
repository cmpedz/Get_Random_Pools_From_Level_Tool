
using Microsoft.Office.Interop.Excel;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace BalloonMasterTool
{
    
    public class ExcelManager
    {
        private Excel._Workbook _workBook;

        private Excel._Application excelApp;

        public ExcelManager(string filePath)
        {
            excelApp = new Excel.Application();
            _workBook = excelApp.Workbooks.Open(filePath);

        }

        public int GetQuantitiesWorkSheet()
        {
            return _workBook.Sheets.Count;
        }

        public void ReleaseMemory()
        {
            _workBook.Close(false);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        public List<string[]> GetDataFromWorkSheet(int workSheetIndex)
        {
            Excel._Worksheet worksheet = _workBook.Sheets[workSheetIndex];
            Excel.Range usedRange = worksheet.UsedRange;

            int rowCount = usedRange.Rows.Count;

            int colCount = usedRange.Columns.Count;

            int firstUsedRow = usedRange.Row;

            int firstUsedCol = usedRange.Column;

            List<string[]> data = new List<string[]>();

            for (int row = 0; row < rowCount - 1; row++)
            {
                data.Add(new string[colCount]);
                for (int col = 0; col < colCount; col++)
                {

                    var cellValue = worksheet.Cells[firstUsedRow + row + 1, firstUsedCol + col].Text;

                    data[row][col] = cellValue;
                }

            }

            return data;

        }

    }
}

