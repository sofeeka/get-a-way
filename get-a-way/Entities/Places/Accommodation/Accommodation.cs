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

    public Accommodation(string name, string location, DateTime openTime, DateTime closeTime, PriceCategory priceCategory, 
        bool petFriendly, AccommodationType type, int maxPeople)
        : base( name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        Type = type;
        Amenities = new HashSet<Amenity>();
        MaxPeople = maxPeople;
        Beds = new Dictionary<BedType, int>();
    }
}