using HouseValueLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HouseValueServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FeedController : ApiController
    {
        private string apiKeyFilename = "apiKey.txt";
        private string apiKey = "";
        private List<House> houses;
        private int count = 0;
        private int trycount = 0;
        public FeedController()
        {
            string outputfile = @"F:\GitHub\HouseValue\Server\Data\housing.tsv";
            var houseslines = File.ReadAllLines(outputfile).Skip(1);
            houses = new List<House>();
            
            foreach (var housesline in houseslines)
            {
                try
                {
                    var house = new House(housesline);
                    houses.Add(house);
                    count++;
                }
                catch (Exception e)
                {
                    trycount++;
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        // POST feed/postarticle
        [HttpPost]
        [Route("feed/postarticle")]
        public IEnumerable<string> myAction()
        {
            return new[] {"value2"};
        }

        [HttpGet]
        [Route("feed/getfeed/{request}")]
        public IEnumerable<string> GetNewsFeed(string request)
        {
            return new[] {"value1", "value2"};
        }

        [HttpPost]
        [Route("feed/getestimate")]
        public async Task<double> GetEstimate([FromBody] Property house)
        {
            apiKey = "";
            var housingData = new HousingData();
            housingData.age_of_property = Convert.ToInt32((DateTime.Now - new DateTime(house.YearConstructed, 1, 1)).TotalDays / 30);
            housingData.bathroom_count = house.Bathrooms;
            housingData.bedroom_count = house.Bedrooms;
            housingData.built_up_area = Convert.ToInt32(house.BuiltUpArea);
            housingData.pincode = house.Pincode == null ? "500027" : house.Pincode;
            housingData.date_of_pricing = DateTime.UtcNow;
            housingData.floor_count = house.FloorCount;
            housingData.floor_number = house.FloorNumber;
            housingData.id = 0;
            housingData.latitude = house.LatLng.Lat;
            housingData.longitude = house.LatLng.Lng;
            housingData.under_construction = house.UnderConstruction;
            var featureVector = Utils.HousingFeatures(housingData).ToArray();
            var value = await ServiceCall.InvokeRequestResponseService(featureVector, apiKey);
            return Convert.ToInt32(1000*(Convert.ToInt32(value * housingData.built_up_area/1000)));
        }

        [HttpPost]
        [Route("feed/getpriceestimate")]
        public async Task<double> GetPriceEstimate([FromBody] House house)
        {
            var comparables = Utils.GetComparables(house, houses).ToArray();
            if (comparables.Length < 6)
            {
                return await GetEstimate(new Property
                {
                    LatLng = new LatLng() { Lat = house.lat, Lng = house.lng},
                    YearConstructed = house.year,
                    BuiltUpArea = house.built_up_area,
                    PlotSize = house.built_up_area,
                    Bathrooms = house.bathroom_count,
                    Bedrooms = house.bedroom_count,
                    Pincode = house.pincode ?? "500027",
                    UnderConstruction = false,
                    FloorCount = house.floor_count,
                    FloorNumber = house.floor_number
                });
            }
            var avg_price_per_sqft = comparables.Average(c => c.price_per_sqft);
            return Convert.ToInt32(1000 * (Convert.ToInt32(avg_price_per_sqft * house.built_up_area / 1000)));
        }


        [HttpPost]
        [Route("feed/getcomparables")]
        public House[] GetComparables([FromBody] House house)
        {
            try
            {
                var comparables = Utils.GetComparables(house, houses).ToArray();
                return comparables;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }

}
