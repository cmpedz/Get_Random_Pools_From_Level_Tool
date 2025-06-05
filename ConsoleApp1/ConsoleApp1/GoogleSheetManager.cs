
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using static Google.Apis.Requests.BatchRequest;
using Excel = Microsoft.Office.Interop.Excel;



namespace BalloonMasterTool
{
    
    public class GoogleSheetManager
    {
        private Excel._Workbook _workBook;

        private Excel._Application excelApp;

        private IList<Sheet> _sheets;

        public GoogleSheetManager(string urlPath)
        {
            //excelApp = new Excel.Application();
            //_workBook = excelApp.Workbooks.Open(filePath);

            string spreadsheetId = Regex.Match(urlPath, @"spreadsheets/d/([a-zA-Z0-9-_]+)").Groups[1].Value;

            Console.WriteLine("check spread sheet id : " + spreadsheetId);

            GoogleCredential credential;
            using (var stream = new FileStream("key.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Candy Master Tool",
            });


            var request = service.Spreadsheets.Get(spreadsheetId);
            request.IncludeGridData = true; // This is the key
            var response = request.Execute();

            _sheets = response.Sheets;

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
            //Excel._Worksheet worksheet = _workBook.Sheets[workSheetIndex];
            //Excel.Range usedRange = worksheet.UsedRange;

            //int rowCount = usedRange.Rows.Count;

            //int colCount = usedRange.Columns.Count;

            //int firstUsedRow = usedRange.Row;

            //int firstUsedCol = usedRange.Column;

            List<string[]> data = new List<string[]>();

            //for (int row = 0; row < rowCount - 1; row++)
            //{
            //    data.Add(new string[colCount]);
            //    for (int col = 0; col < colCount; col++)
            //    {

            //        var cellValue = worksheet.Cells[firstUsedRow + row + 1, firstUsedCol + col].Text;

            //        data[row][col] = cellValue;
            //    }

            //}

            var sheet = _sheets[workSheetIndex - 1];

            var rowData = sheet?.Data?.FirstOrDefault()?.RowData;

            for (int i = 1; i< rowData.Count; i++)
            {
                var row = rowData[i];

                string[] currentRowData = new string[row.Values.Count];

                int index = 0;

                foreach (var cell in row.Values)
                {
                    var value = cell.EffectiveValue;

                    string v = "";

                    if (value != null)
                    {
                        if (value.NumberValue != null)
                        {
                            Console.Write(value.NumberValue + "\t");
                            v = value.NumberValue + "";
                        }
                            
                        else if (value.StringValue != null)
                        {
                            Console.Write(value.StringValue + "\t");
                            v = value.StringValue + "";
                        }
                            
                    }
                    
                    currentRowData[index] = v;
                    index++;
                }
                Console.WriteLine();
                data.Add(currentRowData);
            }

            Console.WriteLine(JsonConvert.SerializeObject(data));

            return data;

        }

    }
}

