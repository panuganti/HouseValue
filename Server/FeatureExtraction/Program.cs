using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseValueLibrary;
using System.IO;
using Newtonsoft.Json;

namespace FeatureExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputDir = @"F:\GitHub\HouseValueServer\Data\Housing";
            string outFile = @"F:\GitHub\HouseValueServer\Data\Features.tsv";
            var allFiles = Directory.GetFiles(inputDir).Where(f => int.Parse(Path.GetFileNameWithoutExtension(f)) < 17389);
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                writer.WriteLine("Id\tPincode\tPropertyType\tFloorCategory\tSqft\tBedrooms\tBathrooms\tStatus\tAge\tPricePerSqft");
                foreach (var file in allFiles)
                {
                    var data =  JsonConvert.DeserializeObject<HousingData>(File.ReadAllText(file));
                    writer.WriteLine(string.Join("\t",Utils.HousingFeatures(data)));
                }
            }
        }
    }
}
