using System;
using System.Runtime.Serialization;

namespace HouseValueLibrary
{
    [DataContract]
    public class HousingData
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public double latitude { get; set; }
        [DataMember]
        public double longitude { get; set; }
        [DataMember]
        public float price_per_sqft { get; set; }
        [DataMember]
        public int floor_count { get; set; } 
        [DataMember]
        public int floor_number { get; set; } 
        [DataMember]
        public int age_of_property { get; set; } //
        [DataMember]
        public bool under_construction { get; set; }
        [DataMember]
        public int built_up_area { get; set; }
        [DataMember]
        public int bedroom_count { get; set; }
        [DataMember]
        public int bathroom_count { get; set; }
        [DataMember]
        public string region_name { get; set; }
        [DataMember]
        public string city_name { get; set; }
        [DataMember]
        public string pincode { get; set; }
        [DataMember]
        public DateTime date_of_pricing { get; set; }
    }

    [DataContract]
    public class MagicBricksData : HousingData
    {
    }

    [DataContract]
    public class HousingComData : HousingData
    {
        [DataMember]
        public string main_entrance_facing { get; set; }
        [DataMember]
        public int hestimate { get; set; }
        [DataMember]
        public int city_id { get; set; }

        public HousingComData(Housing housing)
        {
            if (housing.result.hestimate != null)
            {
                hestimate = housing.result.hestimate.value;
            }
            city_id = housing.result.city_id;
            id = housing.result.id;
            latitude = housing.result.latitude;
            longitude = housing.result.longitude;
            price_per_sqft = housing.result.per_square_feet_rate;
            floor_count = housing.result.floor_count;
            floor_number = housing.result.floor_number;
            age_of_property = housing.result.age_of_property > 0
                ? housing.result.age_of_property
                : Convert.ToInt32(
                    (housing.result.date_added - DateTime.Parse(housing.result.age_of_property_date)).TotalDays/30);
            under_construction = housing.result.under_construction;
            built_up_area = housing.result.built_up_area;
            bedroom_count = housing.result.bedroom_count;
            bathroom_count = housing.result.bathroom_count;
            main_entrance_facing = housing.result.main_entrance_facing;
            region_name = housing.result.region_name;
            city_name = housing.result.city_name;
            date_of_pricing = housing.result.date_added;
        }
    }
}
