using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
// ReSharper disable AssignNullToNotNullAttribute

namespace ParseHousingData
{
    class Program
    {
        static void Main(string[] args)
        {
            string housingDir = @"F:\OldComputer\E\NMW\DataScraping\HousingData\ScrapingHousingData\ScrapingHousingData\HousingSaleData";
            string outputfile = @"F:\GitHub\HouseValue\Server\Data\housing.tsv";
            GenerateHousingTsv(housingDir, outputfile);
        }

        private static void GenerateHousingTsv(string inputDir, string outfile)
        {
            var files = Directory.GetFiles(inputDir).OrderBy(f => int.Parse(Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(f)))).ToArray();
            var writer = new StreamWriter(outfile);
            writer.WriteLine(string.Join("\t", new[]
                {
                    "id",
                    "latitude",
                    "longitude",
                    "center_point_latitude",
                    "center_point_longitude",
                    "street_info",
                    "locality",
                    "price",
                    "per_square_feet_rate",
                    "floor_count",
                    "floor_number",
                    "apartment_type",
                    "property_type",
                    "date_added",
                    "age_of_property",
                    "age_of_property_date",
                    "under_construction",
                    "built_up_area",
                    "bedroom_count",
                    "bathroom_count",
                    "is_price_negotiable",
                    "has_swimming_pool",
                    "has_gym",
                    "number_of_lifts",
                    "parking_count",
                    "seo_address_tags",
                    "seo_title",
                    "main_entrance_facing",
                    "region_name",
                    "city_id",
                    "has_gas_pipeline",
                    "water_supply_type",
                    "has_servant_room",
                    "power_backup_type",
                    "is_gated_community",
                    "security_type",
                    "is_society_formed",
                    "lifestyle_rating",
                    "lifestyle_rating",
                    "society_rating",
                    "location_rating",
                    "lifestyle_rating_type",
                    "connectivity_score",
                    "location_score",
                    "peripheral_score",
                    "locality_type",
                    "society_type",
                    "poshness_index",
                    "city_name",
                    "brokers.number",
                    "main_entrance_facing",
                    "contact_persons.number",
                    "contact_persons.original_number",
                    "contact_persons.profile_type",
                    "owner_type",
                    "contact_person_type",
                    "hestimate.label",
                    "hestimate.value"
                }));

            foreach (var file in files)
            {
                var id = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(file));
                Console.WriteLine("Processing {0}", id);
                var housingJson = File.ReadAllText(file);
                var housing = JsonConvert.DeserializeObject<Housing>(housingJson);
                if (housing?.result == null)
                {
                    Console.WriteLine("Skipping {0}", id); continue;
                }

                writer.WriteLine(string.Join("\t", new []
                {                    
                    housing.result.id,
                    housing.result.latitude,
                    housing.result.longitude,
                    housing.result.center_point_latitude,
                    housing.result.center_point_longitude,
                    housing.result.street_info,
                    housing.result.locality,
                    housing.result.price,
                    housing.result.per_square_feet_rate,
                    housing.result.floor_count,
                    housing.result.floor_number,
                    housing.result.apartment_type,
                    housing.result.property_type,
                    housing.result.date_added,
                    housing.result.age_of_property,
                    housing.result.age_of_property_date,
                    housing.result.under_construction,
                    housing.result.built_up_area,
                    housing.result.bedroom_count,
                    housing.result.bathroom_count,
                    housing.result.is_price_negotiable,
                    housing.result.has_swimming_pool,
                    housing.result.has_gym,
                    housing.result.number_of_lifts,
                    housing.result.parking_count,
                    housing.result.seo_address_tags == null ? "" : string.Join(",",housing.result.seo_address_tags),                    
                    housing.result.seo_title,
                    housing.result.main_entrance_facing,
                    housing.result.region_name,
                    housing.result.city_id,
                    housing.result.has_gas_pipeline,
                    housing.result.water_supply_type ?? "99",
                    housing.result.has_servant_room,
                    housing.result.power_backup_type ?? "",
                    housing.result.is_gated_community,
                    housing.result.security_type,
                    housing.result.is_society_formed ?? "",
                    housing.result.lifestyle_rating,
                    housing.result.lifestyle_fields?.lifestyle_rating.ToString() ?? "",
                    housing.result.lifestyle_fields?.society_rating.ToString(CultureInfo.InvariantCulture) ?? "",
                    housing.result.lifestyle_fields?.location_rating.ToString(CultureInfo.InvariantCulture) ?? "",
                    housing.result.lifestyle_fields == null ? "" : housing.result.lifestyle_fields.lifestyle_rating_type,
                    housing.result.lifestyle_fields?.connectivity_score.ToString(CultureInfo.InvariantCulture) ?? "",
                    housing.result.lifestyle_fields?.location_score.ToString(CultureInfo.InvariantCulture) ?? "",
                    housing.result.lifestyle_fields?.peripheral_score.ToString(CultureInfo.InvariantCulture) ?? "",
                    housing.result.lifestyle_fields == null ? "" : housing.result.lifestyle_fields.locality_type,
                    housing.result.lifestyle_fields == null ? "" : housing.result.lifestyle_fields.society_type,
                    housing.result.lifestyle_fields == null ? "" : housing.result.lifestyle_fields.poshness_index ?? "",
                    housing.result.city_name,
                    housing.result.brokers.Any(b => b.number != null ) ? string.Join(",",housing.result.brokers.Where(b => b.number != null).SelectMany(b => b.number).ToArray()) : "",
                    housing.result.main_entrance_facing,
                    housing.result.contact_persons.Any(b => b.number != null) ?  string.Join(",",housing.result.contact_persons.Where(b => b.number != null).Select(b => b.number)) : "",
                    housing.result.contact_persons.Any(b => b.original_number != null) ? string.Join(",",housing.result.contact_persons.Where(b => b.original_number != null).Select(b => b.original_number)) : "",
                    string.Join(",",housing.result.contact_persons.Select(b => b.profile_type)),
                    housing.result.owner_type,
                    housing.result.contact_person_type,
                    housing.result.hestimate == null ? "" : housing.result.hestimate.label,
                    housing.result.hestimate?.value.ToString() ?? ""
                }));
            }
            writer.Close();
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
