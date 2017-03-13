using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HouseValueLibrary
{
    public class Housing
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Result result { get; set; }
    }

    public class Result
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float latitude { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float longitude { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float center_point_latitude { get; set; } //2. 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float center_point_longitude { get; set; } // 3. 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string street_info { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string locality { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string formatted_price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int per_square_feet_rate { get; set; } // 1. 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int floor_count { get; set; } // 4.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int floor_number { get; set; } // 5. 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string apartment_type { get; set; } // 6
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string property_type { get; set; } // 7
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime date_added { get; set; } //8
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string available_from { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int age_of_property { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string age_of_property_date { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool under_construction { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string thumb_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string thumb_name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string thumb_url { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string thumb_url_new { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object[] floor_plan_images { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object[] floor_plan_images_new { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int built_up_area { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int bedroom_count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int bathroom_count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool is_price_negotiable { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool has_swimming_pool { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool has_gym { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int number_of_lifts { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int parking_count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] seo_address_tags { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string seo_title { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string title { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object display_regions { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string main_entrance_facing { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int image_count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[][] images { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[][] images_new { get; set; }
        /*
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Images_With_Master_Tags[] images_with_master_tags { get; set; }
        */
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string region_name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string share_url { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string canonical_url { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string locality_url_name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string locality_filter_param { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int fi { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int city_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool has_gas_pipeline { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string water_supply_type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool has_servant_room { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string power_backup_type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool is_gated_community { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string security_type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object is_society_formed { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Lifestyle_Fields lifestyle_fields { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string lifestyle_rating { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string city_name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Broker[] brokers { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string owner_type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Contact_Persons[] contact_persons { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string contact_person_type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Hestimate hestimate { get; set; }
    }

    public class Lifestyle_Fields
    {
        public int lifestyle_rating { get; set; }
        public float society_rating { get; set; }
        public float location_rating { get; set; }
        public string lifestyle_rating_type { get; set; }
        public float connectivity_score { get; set; }
        public float location_score { get; set; }
        public float peripheral_score { get; set; }
        public string locality_type { get; set; }
        public string society_type { get; set; }
        public object poshness_index { get; set; }
        public Connectivity connectivity { get; set; }
        public Neighbourhood neighbourhood { get; set; }
        public float neighbourhood_score { get; set; }
    }

    public class Connectivity
    {
        public string[] Good { get; set; }
        public string[] None { get; set; }
        public string[] Great { get; set; }
    }

    public class Neighbourhood
    {
        public string[] Good { get; set; }
    }

    public class Hestimate
    {
        public string label { get; set; }
        public int value { get; set; }
        public object formatted_value { get; set; }
    }

    public class Images_With_Master_Tags
    {
        public string master_tag { get; set; }
        public Image[] images { get; set; }
    }

    public class Image
    {
        public string absolute_url { get; set; }
        public string caption { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool is_landscape { get; set; }
    }

    public class Broker
    {
        public int id { get; set; }
        public string name { get; set; }
        public string firm { get; set; }
        public object[] number { get; set; }
        public string experience { get; set; }
        public Locality[] localities { get; set; }
        public Service[] services { get; set; }
        public string firm_legal_status { get; set; }
        public string image_url { get; set; }
        public string image_url_new { get; set; }
    }

    public class Locality
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Service
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Contact_Persons
    {
        public int id { get; set; }
        public int profile_id { get; set; }
        public string name { get; set; }
        public string firm_name { get; set; }
        public object number { get; set; }
        public object original_number { get; set; }
        public string profile_url { get; set; }
        public string image_url { get; set; }
        public string image_url_new { get; set; }
        public string profile_type { get; set; }
    }
}
