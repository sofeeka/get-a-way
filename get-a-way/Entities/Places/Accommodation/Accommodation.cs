namespace get_a_way.Entities.Places.Accommodation;

[Serializable]
public class Accommodation : Place
{
    public AccommodationType Type { get; set; }
    public HashSet<Amenity> Amenities { get; set; }
    public int MaxPeople { get; set; }
    public Dictionary<BedType, int> Beds { get; set; }

    public Accommodation() 
    {
    }

    // TODO initialise class Place fields
    public Accommodation(AccommodationType type, int maxPeople)
    {
        Type = type;
        Amenities = new HashSet<Amenity>();
        MaxPeople = maxPeople;
        Beds = new Dictionary<BedType, int>();
    }
}