
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BalloonMasterTool
{
    namespace BalloonMatrix
    {
        public class BalloonMatrix3D : IToJson
        {
            private List<BalloonMatrix2D> _balloonMatrix2Ds;
            public BalloonMatrix3D(List<BalloonMatrix2D> balloonMatrix2Ds) 
            { 
                _balloonMatrix2Ds = balloonMatrix2Ds;
            }

            public string ToJson()
            {
                List<List<string[]>> balloonMatrix3D = new List<List<string[]>>();

                Dictionary<string, List<List<string[]>>> candyMatrixDataToDict = new Dictionary<string, List<List<string[]>>>();

                foreach (BalloonMatrix2D balloonMatrix2D in _balloonMatrix2Ds) 
                {
                    balloonMatrix3D.Add(balloonMatrix2D.GetData());
                }

                candyMatrixDataToDict.Add("candyMatrix", balloonMatrix3D);

                return JsonConvert.SerializeObject(candyMatrixDataToDict, Formatting.Indented);
            }

            public void print()
            {
                Console.WriteLine(ToJson());
            }
        }
    }
}
