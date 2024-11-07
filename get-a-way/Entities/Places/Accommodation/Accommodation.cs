using System.Xml.Serialization;

namespace get_a_way.Entities.Places.Accommodation;

[Serializable]
public class Accommodation : Place
{
    private AccommodationType _type;
    private HashSet<Amenity> _amenities;
    private int _maxPeople;

    private Dictionary<BedType, int> _beds;

    public AccommodationType Type
    {
        get => _type;
        set => _type = value;
    }

    public HashSet<Amenity> Amenities
    {
        get => _amenities;
        set => _amenities = value;
    }

    public int MaxPeople
    {
        get => _maxPeople;
        set => _maxPeople = value;
    }

    [XmlArray("BedEntries")]
    [XmlArrayItem("BedEntry")]
    public List<BedEntry> BedEntries
    {
        get
        {
            var entries = new List<BedEntry>();
            foreach (var kvp in _beds)
                entries.Add(new BedEntry { BedType = kvp.Key, Count = kvp.Value });

            return entries;
        }
        set
        {
            if (value != null)
            {
                _beds = new Dictionary<BedType, int>();
                foreach (var entry in value)
                    _beds[entry.BedType] = entry.Count;
            }
        }
    }

    public Accommodation()
    {
    }

    public Accommodation(string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory,
        bool petFriendly, AccommodationType type, int maxPeople)
        : base(name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        Type = type;
        Amenities = new HashSet<Amenity>();
        MaxPeople = maxPeople;
        BedEntries = new List<BedEntry>();
    }
}