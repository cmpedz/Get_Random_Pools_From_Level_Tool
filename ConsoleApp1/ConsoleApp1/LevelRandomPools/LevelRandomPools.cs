using BalloonMasterTool;
using Newtonsoft.Json;

namespace ConsoleApp1.LevelRandomPools
{
    public class LevelRandomPools
    {
        public int levelId;

        public Dictionary<string, RandomPool> randomPoolsDict = new Dictionary<string, RandomPool>();

        public LevelRandomPools(int levelId)
        {
            this.levelId = levelId;
        }

        public void ExportResult(string resultPath)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            foreach(var poolName in randomPoolsDict.Keys)
            {
                Console.WriteLine("check pool name : " + poolName);

                result.Add(poolName, randomPoolsDict[poolName].GetCandiesData());
            }

            File.WriteAllText(resultPath, JsonConvert.SerializeObject(result));
        }

        public void ReadRandomPoolsDataFromExcel(List<string[]> data)
        {
            foreach (var item in data) 
            {
                if (item[0].Length == 0) continue;

                string poolName = "Pool_" + item[0];                

                string uv = item[1];

                string type = item[2];  

                string color = item[3];

                string number = item[4];

                CandyType candy = new CandyType(uv, type, color, number);

                AddCandiesIntoRandomPool(poolName, candy);

            }
        }

        private void AddCandiesIntoRandomPool(string poolName, CandyType candy)
        {
            if (!randomPoolsDict.ContainsKey(poolName))
            {
                randomPoolsDict.Add(poolName, new RandomPool(poolName));
            }

            randomPoolsDict[poolName].AddCandies(candy);

        }

    }
}
