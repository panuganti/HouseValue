using System.Runtime.Serialization;

namespace GoogleDatastore
{
    [DataContract]
    public class Property
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public PropertyType PropertyType { get; set; }

        [DataMember]
        public string ConstructionStatus { get; set; }

        [DataMember]
        public int YearConstructed { get; set; }

        [DataMember]
        public int BuiltUpArea { get; set; }

        [DataMember]
        public int PlotSize { get; set; }

        [DataMember]
        public AreaUnits AreaUnits { get; set; }

        [DataMember]
        public bool NewProperty { get; set; }
    }

    [DataContract]
    public enum AreaUnits
    {
        SqFt,
        SqYards,
        Acre,
        Bigha,
        Hectare,
        Kanal,
        Guntha,
        Are,
        Perch,
        Cent,
        Marla,
        Kottah,
        Chatak,
        Rood,
        Aankadam,
        Biswa,
        SqM
    }

    [DataContract]
    public class Location
    {
        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Locality { get; set; }

        [DataMember]
        public Geo Geo { get; set; }
    }

    [DataContract]
    public class Geo
    {
        [DataMember]
        public double Lat { get; set; }

        [DataMember]
        public double Lng { get; set; }
    }

    [DataContract]
    public class PropertyFeatures
    {
        [DataMember]
        public int Bedrooms { get; set; }

        [DataMember]
        public double Bathrooms { get; set; }

        [DataMember]
        public bool Furnished { get; set; }

        [DataMember]
        public int FloorNo { get; set; }

        [DataMember]
        public int TotalFloors { get; set; }
    }

    [DataContract]
    public class VillaArea
    {
        [DataMember]
        public int CoveredArea { get; set; }

        [DataMember]
        public int PlotArea { get; set; }

        [DataMember]
        public int CarpetArea { get; set; }
    }

    [DataContract]
    public class ApartmentArea
    {
        [DataMember]
        public int SuperBuiltupArea { get; set; }

        [DataMember]
        public int BuiltupArea { get; set; }

        [DataMember]
        public int CarpetArea { get; set; }
    }

    [DataContract]
    public enum PropertyType
    {
        Residential,
        Commercial,
        Agricultural
    }
}
