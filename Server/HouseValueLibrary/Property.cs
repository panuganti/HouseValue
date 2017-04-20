using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HouseValueLibrary
{
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
