using BalloonMasterTool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.LevelRandomPools
{
    public class RandomPool
    {
        public string poolName = "";

        public List<CandyType> candies = new List<CandyType>();

        public RandomPool(string poolName)
        {
            this.poolName = poolName;
        }

        public void AddCandies(CandyType candy)
        {
            candies.Add(candy);
        }

        public List<string> GetCandiesData()
        {
            List<string> candiesTypeFormat = new List<string>();

            foreach(CandyType candy in candies)
            {
                candiesTypeFormat.Add(candy.GetCandyType());
            }

            return candiesTypeFormat;
        }
    }
}
