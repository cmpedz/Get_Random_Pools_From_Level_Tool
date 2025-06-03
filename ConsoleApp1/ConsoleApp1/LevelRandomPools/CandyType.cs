using System;

namespace ConsoleApp1.LevelRandomPools
{
    public class CandyType
    {
        public string uVNumber = "";
        public string type = "";
        public string color = "";
        public string number = "";

        public CandyType(string uVNumber, string type, string color, string number)
        {
            this.uVNumber = uVNumber;
            this.type = type;
            this.color = color;
            this.number = number;
        }

        public string StandardName(string name)
        {
            if (name.Length == 0) return "";

            string firstChar = name.Substring(0, 1).ToUpper();

            return firstChar + name.Remove(0,1);

        }

        public string GetCandyType()
        {
            return "UV" + uVNumber + StandardName(color) + StandardName(type) + number;
        }
    }
}
