
using BalloonMasterTool.BalloonMatrix;
using ConsoleApp1.LevelRandomPools;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace BalloonMasterTool
{
    public class Tool
    {
        private const string EXCEL_PATH = "C:\\Users\\Admin\\Downloads\\Data level 2.xlsx";

        private const string RESULT_FOLDER = "Level";

        private const string JSON_FILE_NAME = "Level";
        static void Main(string[] args)
        {
            
            Console.Write("Design for level : ");
            string levelIndex = Console.ReadLine();

            //read data from excel 
            ExcelManager excelManager = new ExcelManager(EXCEL_PATH);

            Console.WriteLine("check sheet get : " + int.Parse(levelIndex));

            List<string[]> data = excelManager.GetDataFromWorkSheet(int.Parse(levelIndex));

            LevelRandomPools level = new LevelRandomPools(int.Parse(levelIndex));

            level.ReadRandomPoolsDataFromExcel(data);

            //save json file into assigned folder
            string rootFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.ToString();

            string jsonFileName = JSON_FILE_NAME + "_" + levelIndex + ".json";

            string filePath = Path.Combine([rootFolder, RESULT_FOLDER, jsonFileName]) ;

            level.ExportResult(filePath);

            Console.WriteLine("======================================================");

            Console.WriteLine("End tool");

            Console.WriteLine("result is saved in : " + filePath);

            Console.ReadLine();

            excelManager.ReleaseMemory();
        }
    }
}
