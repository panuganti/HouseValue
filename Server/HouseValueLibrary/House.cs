using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace HouseValueLibrary
{
    [DataContract]
    public class House
    {
        [DataMember]
        public string city_name { get; set; }
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }
        [DataMember]
        public int year { get; set; }
        [DataMember]
        public string locality { get; set; }
        [DataMember]
        public int floor_count { get; set; }
        [DataMember]
        public int floor_number { get; set; }
        [DataMember]
        public string property_type { get; set; }
        [DataMember]
        public int date_priced { get; set; }
        [DataMember]
        public int built_up_area { get; set; }
        [DataMember]
        public int bedroom_count { get; set; }
        [DataMember]
        public int bathroom_count { get; set; }
        [DataMember]
        public string main_entrance_facing { get; set; }
        [DataMember]
        public bool is_gated_community { get; set; }
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double price_per_sqft { get; set; }
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string pincode { get; set; }


        public House()
        {
        }
        

        public House(string line)
        {
            var cols = line.Split('\t');
            city_name = cols[82 - 34];
            lat = double.Parse(cols[37 - 34]);
            lng = double.Parse(cols[38 - 34]);
            year = DateTime.Parse(cols[49 - 34]).Subtract(TimeSpan.FromDays(int.Parse(cols[48 - 34]) * 365)).Year;
            locality = cols[40 - 34];
            floor_count = int.Parse(cols[43 - 34]);
            floor_number = int.Parse(cols[44 - 34]);
            property_type = cols[46 - 34];
            date_priced = DateTime.Parse(cols[49 - 34]).Year;
            built_up_area = int.Parse(cols[51 - 34]);
            bedroom_count = int.Parse(cols[52 - 34]);
            bathroom_count = int.Parse(cols[53 - 34]);
            main_entrance_facing = cols[84 - 34];
            is_gated_community = bool.Parse(cols[68 - 34]);
            price_per_sqft = double.Parse(cols[42 - 34]);
        }
        
    }

    /*

    [DataContract]
    public class House
    {
        [DataMember]
        public string city_name { get; set; }
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }
        [DataMember]
        public int year { get; set; }
        [DataMember]
        public string locality { get; set; }
        [DataMember]
        public int floor_count { get; set; }
        [DataMember]
        public int floor_number { get; set; }
        [DataMember]
        public string property_type { get; set; }
        [DataMember]
        public int date_priced { get; set; }
        [DataMember]
        public int built_up_area { get; set; }
        [DataMember]
        public int bedroom_count { get; set; }
        [DataMember]
        public int bathroom_count { get; set; }
        [DataMember]
        public string main_entrance_facing { get; set; }
        [DataMember]
        public bool is_gated_community { get; set; }

        public House(string line)
        {
            var cols = line.Split('\t');
            city_name = cols[82 - 34];
            lat = double.Parse(cols[37 - 34]);
            lng = double.Parse(cols[38 - 34]);
            year = DateTime.Parse(cols[49 - 34]).Subtract(TimeSpan.FromDays(int.Parse(cols[48-34])*365)).Year;
            locality = cols[40 - 34];
            floor_count = int.Parse(cols[43 - 34]);
            floor_number = int.Parse(cols[44 - 34]);
            property_type = cols[46 - 34];
            date_priced = DateTime.Parse(cols[49 - 34]).Year;
            built_up_area = int.Parse(cols[51 - 34]);
            bedroom_count = int.Parse(cols[52 - 34]);
            bathroom_count = int.Parse(cols[53 - 34]);
            main_entrance_facing = cols[84 - 34];
            is_gated_community = bool.Parse(cols[68 - 34]);
        }
    }
    */
}
