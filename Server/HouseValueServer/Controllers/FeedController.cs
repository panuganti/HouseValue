using HouseValueLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using HouseValueLibrary;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace HouseValueServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FeedController : ApiController
    {
        private string apiKeyFilename = "apiKey.txt";
        private string apiKey = "";

        public FeedController()
        {
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
    }

    [DataContract]
    public class Test
    {
        [DataMember]
        public string Id { get; set; }
    }


    [DataContract]
    public class Property
    {
        [DataMember]
        public LatLng LatLng { get; set; }
        [DataMember]
        public int YearConstructed { get; set; }
        [DataMember]
        public double BuiltUpArea { get; set; }
        [DataMember]
        public double PlotSize { get; set; }
        [DataMember]
        public int Bathrooms { get; set; }
        [DataMember]
        public int Bedrooms { get; set; }
        [DataMember]
        public string Pincode { get; set; }
        [DataMember]
        public bool UnderConstruction { get; set; }
        [DataMember]
        public int FloorCount { get; set; }
        [DataMember]
        public int FloorNumber { get; set; }
    }

    [DataContract]
    public enum PropertyType
    {
        [EnumMember]
        MultistoreyApartment,
        [EnumMember]
        BuilderFloorApartment,
        [EnumMember]
        ResidentialHouse,
        [EnumMember]
        Villa,
        [EnumMember]
        ResidentialPlot,
        [EnumMember]
        Penthouse,
        [EnumMember]
        Studio
    }

    [DataContract]
    public class LatLng
    {
        [DataMember]
        public double Lat { get; set; }
        [DataMember]
        public double Lng { get; set; }
    }
}
