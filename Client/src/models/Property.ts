export class Property {
    public LatLng: Geo;
    public YearConstructed: number;
    public BuiltUpArea: number;
    public PlotSize: number;
    public Bedrooms: number;
    public Bathrooms: number;
    public Pincode: string;
    public UnderConstruction: boolean;
    public FloorCount: number;
    public FloorNumber: number;
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
