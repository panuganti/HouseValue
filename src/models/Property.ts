export class Property {
    public LatLng: Geo;
    public YearConstructed: number;
    public BuiltUpArea: number;
    public PlotSize: number;
    public Bedrooms: number;
    public Bathrooms: number;
}

export class Geo
{
    public Lat: number;
    public Lng: number;
}

export enum PropType {
    MultistoreyApartment,
    BuilderFloorApartment,
    ResidentialHouse,
    Villa,
    ResidentialPlot,
    Penthouse,
    Studio
}

/*
export enum AreaUnits {
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

export class ResidentialUnit {
    public ConstructionStatus: string;
    public YearConstructed: number;
    public BuiltUpArea: number;
    public AreaUnits: AreaUnits;
    public PropertyFeatures: PropertyFeatures;
}

export class Location
{
    public City:string;
    public Locality: string;
    public Geo: Geo;
}

export class PropertyFeatures
{
    public Bedrooms: number;
    public Bathrooms: number;
    public Furnished: boolean;
    public FloorNo: number;
    public TotalFloors: number;
}

// Villa, BuilderFloorApartment, House,
export class VillaArea{
    public CoveredArea: number;
    public PlotArea: number;
    public CarpetArea: number;
}

export class ApartmentArea {
    public SuperBuiltupArea: number;
    public BuiltupArea: number;
    public CarpetArea: number;
}


export enum CommercialUnitType {
    CommercialOfficeSpace,
    OfficeinITParkSEZ,
    CommercialShop,
    CommercialShowroom,
    CommercialLand,
    WarehouseGodown,
    IndustrialLand,
    IndustrialBuilding,
    IndustrialShed,
}

export enum AgriculturalUnitType {
    Land,
    FarmHouse
}

export enum PropertyType {
    Residential,
    Commercial,
    Agricultural
}
*/
