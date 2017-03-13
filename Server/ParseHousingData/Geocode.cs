using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ParseHousingData
{
    [DataContract]
    public class GeoCode
    {
        [DataMember]
        public GeocodeResult[] results { get; set; }
        [DataMember]
        public string status { get; set; }
    }

    [DataContract]
    public class GeocodeResult
    {
        [DataMember]
        public Address_Components[] address_components { get; set; }
        [DataMember]
        public string formatted_address { get; set; }
        [DataMember]
        public Geometry geometry { get; set; }
        [DataMember]
        public string place_id { get; set; }
        [DataMember]
        public string[] types { get; set; }
    }

    [DataContract]
    public class Geometry
    {
        [DataMember]
        public Location location { get; set; }
        [DataMember]
        public string location_type { get; set; }
        [DataMember]
        public Viewport viewport { get; set; }
    }

    [DataContract]
    public class Location
    {
        [DataMember]
        public float lat { get; set; }
        [DataMember]
        public float lng { get; set; }
    }

    [DataContract]
    public class Viewport
    {
        [DataMember]
        public Northeast northeast { get; set; }
        [DataMember]
        public Southwest southwest { get; set; }
    }

    [DataContract]
    public class Northeast
    {
        [DataMember]
        public float lat { get; set; }
        [DataMember]
        public float lng { get; set; }
    }

    [DataContract]
    public class Southwest
    {
        [DataMember]
        public float lat { get; set; }
        [DataMember]
        public float lng { get; set; }
    }

    [DataContract]
    public class Address_Components
    {
        [DataMember]
        public string long_name { get; set; }
        [DataMember]
        public string short_name { get; set; }
        [DataMember]
        public string[] types { get; set; }
    }
}
