using Newtonsoft.Json;
using System.Xml;

namespace BalloonMasterTool
{
    namespace BalloonMatrix
    {
        public class BalloonMatrix2D : IToJson 
        {
            private List<string[]> data;
            public BalloonMatrix2D(List<string[]> data) 
            { 
                this.data = data;
            }

            public List<string[]> GetData()
            {
                return data;
            }

            public string ToJson()
            {
                return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            }

            public void print()
            {
                Console.WriteLine(ToJson());
            }
        }
    }
}
