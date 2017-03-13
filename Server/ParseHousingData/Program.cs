using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Threading;
using HouseValueLibrary;

namespace ParseHousingData
{
    class Program
    {
        static void Main(string[] args)
        {
            string housingDir = @"F:\OldComputer\E\NMW\DataScraping\HousingData\ScrapingHousingData\ScrapingHousingData\HousingSaleData";
            string outputDir = @"F:\GitHub\HouseValueServer\Data\Housing";
            ParseHousingData(housingDir, outputDir);
        }

        static void ParseHousingData(string inputDir, string outputDir)
        {

            var files = Directory.GetFiles(inputDir).OrderBy(f => int.Parse(Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(f)))).ToArray();
            foreach(var file in files)
            {
                Console.WriteLine("Processing {0}", Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(file)));
                if (int.Parse(Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(file))) < 17388 ){ continue;}
                var housingJson = File.ReadAllText(file);
                var housing = JsonConvert.DeserializeObject<Housing>(housingJson);
                if (housing?.result == null) { continue;}
                if (housing.result.id > 100000 && housing.result.id < 100507) { continue; }
                var housingData = new HousingComData(housing);
                var data = GetData(new Location() { lat = housing.result.latitude, lng = housing.result.longitude });
                if (data.pincode == null) continue;                
                housingData.pincode = data.pincode;
                housingData.city_name = data.city_name ?? housingData.city_name;
                housingData.region_name = data.region_name ?? housingData.region_name;
                File.WriteAllText(Path.Combine(outputDir, Path.GetFileNameWithoutExtension(file)), JsonConvert.SerializeObject(housingData, Formatting.Indented));
                Thread.Sleep(2000);
            }
        }

        static void ParseMagicBricksData(string inputDir, string outputDir)
        {
            var files = Directory.GetFiles(inputDir);
            foreach (var file in files)
            {
                var housingData = new MagicBricksData();
                housingData.id = int.Parse(Path.GetFileNameWithoutExtension(file));
                var magicBricksHtml = File.ReadAllText(file);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(magicBricksHtml);
                var detailNode = htmlDoc.GetElementbyId("detailOverview");
                var headerNode = htmlDoc.GetElementbyId("detailHeaderFixed");
                var detailNodes = GetNodesOfAClass(detailNode, "aboutDetail");

                var detailAddress = GetNodesOfAClass(headerNode, "detailAddress").First().InnerText;
                string address = detailAddress;
                var data = GetLocationData(address);
                housingData.city_name = data.city_name;
                housingData.pincode = data.pincode;
                housingData.region_name = data.region_name;
                housingData.latitude = data.location.lat;
                housingData.longitude = data.location.lng;

                foreach (var node in detailNodes)
                {
                    var leftColText = GetNodesOfAClass(node, "leftCol").First().InnerText;
                    var rightColText = GetNodesOfAClass(node, "rightCol").First().InnerText;
                    switch (leftColText)
                    {
                        case "Configuration":
                            housingData.bedroom_count = int.Parse(rightColText);
                            housingData.bathroom_count = int.Parse(rightColText);
                            break;
                        case "Area":
                            housingData.built_up_area = int.Parse(rightColText);
                            break;
                        case "Status":
                            housingData.under_construction = bool.Parse(rightColText);
                            break;
                        case "Floor Number":
                            housingData.floor_number = int.Parse(rightColText);
                            housingData.floor_count = int.Parse(rightColText);
                            break;
                        case "Price":
                            housingData.price_per_sqft = int.Parse(rightColText);
                            break;
                        default: continue;
                    }
                }
                housingData.price_per_sqft = housingData.price_per_sqft / housingData.built_up_area;
                housingData.age_of_property = 0;

                if (!CheckMagicBricksValidity(housingData))
                {
                    Console.WriteLine("Data incomplete");
                    continue;
                }
                File.WriteAllText(Path.Combine(outputDir, string.Format("{0}.json", Path.GetFileNameWithoutExtension(file))), JsonConvert.SerializeObject(housingData));
            }
        }

        static Data GetData(Location loc)
        {
            try
            {
                return GetLocationData(loc);
            }
            catch (Exception)
            {
                Thread.Sleep(120000);
                return GetLocationData(loc);
            }
        }

        static Data GetLocationData(Location loc)
        {                        
            var requestUri = $"http://maps.google.com/maps/api/geocode/json?latlng={loc.lat},{loc.lng}&key=AIzaSyCg3p11jiOK4-9_e5Gt6Q683wubEWfT8SA";

            var request = WebRequest.Create(requestUri);
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                var geoOutput = JsonConvert.DeserializeObject<GeoCode>(responseString);
                if (geoOutput.status == "OVER_QUERY_LIMIT")
                {
                    Thread.Sleep(120000);
                    throw new Exception("Query Over Limit");
                }
                var geo = new Location
                {
                    lat = geoOutput.results[0].geometry.location.lat,
                    lng = geoOutput.results[0].geometry.location.lng
                };
                var city =
                    geoOutput.results[0].address_components.FirstOrDefault(
                        t => t.types.Contains("locality") && t.types.Contains("political"));
                var pincode = geoOutput.results[0].address_components.FirstOrDefault(t => t.types.Contains("postal_code"));
                var region =
                    geoOutput.results[0].address_components.FirstOrDefault(
                        t => t.types.Contains("sublocality_level_1") && t.types.Contains("sublocality"));
                var data = new Data
                {
                     
                    city_name = city?.short_name,
                    pincode = pincode?.short_name,
                    region_name = region?.short_name,
                    location = geo
                };
                return data;
            }
        }

        static Data GetLocationData(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=true", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                var geoOutput = JsonConvert.DeserializeObject<GeoCode>(responseString);
                var geo = new Location
                {
                    lat = geoOutput.results[0].geometry.location.lat,
                    lng = geoOutput.results[0].geometry.location.lng
                };
                var data = new Data
                {
                    city_name = geoOutput.results[0].address_components.First(t => t.types.Contains("locality") && t.types.Contains("political")).short_name,
                    pincode = geoOutput.results[0].address_components.First(t => t.types.Contains("postal_code")).short_name,
                    region_name = geoOutput.results[0].address_components.First(t => t.types.Contains("sublocality_level_1") && t.types.Contains("sublocality")).short_name,
                    location = geo
                };
                return data;
            }
        }

        public static bool CheckMagicBricksValidity(MagicBricksData data)
        {
            if ((!data.under_construction && data.age_of_property == 0) || data.bedroom_count == 0 || data.bathroom_count == 0 || data.latitude == 0 || data.longitude == 0 || data.id == 0)
            {
                return false;
            }
            return true;
        }

        public static IEnumerable<HtmlNode> GetNodesOfAClass(HtmlNode node, string className)
        {
            return node.Descendants()
                .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals(className));
        }
    }

    public class Data
    {
        public Location location { get; set; }
        public string city_name { get; set; }
        public string region_name { get; set; }
        public string pincode { get; set; }
    }
}
