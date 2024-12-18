using System.Xml.Serialization;
using get_a_way.Exceptions;

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
        set => _amenities = ValidateAmenities(value);
    }

    public int MaxPeople
    {
        get => _maxPeople;
        set => _maxPeople = ValidateMaxPeople(value);
    }

    
    [XmlArray("Beds")]
    [XmlArrayItem("Bed")]
    public List<KeyValuePair<BedType, int>> Beds => _beds.ToList();

    public Accommodation()
    {
    }

    public Accommodation(string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly, AccommodationType type, int maxPeople)
        : base(name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        Type = type;
        Amenities = new HashSet<Amenity>();
        MaxPeople = maxPeople;
        _amenities = new HashSet<Amenity>();
        _beds = new Dictionary<BedType, int>();
    }

    private HashSet<Amenity> ValidateAmenities(HashSet<Amenity> value)
    {
        if (value == null)
            throw new InvalidAttributeException("Amenities list cannot be null");
        return value;
    }

    private int ValidateMaxPeople(int value)
    {
        if (value <= 0)
            throw new InvalidAttributeException("Max count of people in the accommodation must be greater than 0");
        return value;
    }
    
    public void AddBed(BedType bedType, int count)
    {
        if (count <= 0)
            throw new InvalidAttributeException("Bed count must be greater than zero");

        if (_beds.ContainsKey(bedType))
            _beds[bedType] += count; 
        else
            _beds[bedType] = count;
    }

    public void RemoveBed(BedType bedType)
    {
        if (!_beds.ContainsKey(bedType))
            throw new KeyNotFoundException("No bed entry found for the specified BedType");

        _beds.Remove(bedType);
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Accommodation Type: {Type}\n" +
               $"Max People: {MaxPeople}\n" +
               $"Amenities: {GetAmenitiesString()}\n" +
               $"Beds: {GetBedsString()}\n";
    }

    private string GetAmenitiesString()
    {
        if (Amenities.Count == 0)
            return "No amenities available";
        return string.Join(", ", Amenities);
    }

    private string GetBedsString()
    {
        return _beds.Count == 0
            ? "No beds available"
            : string.Join(", ", _beds.Select(b => $"{b.Key} x {b.Value}"));
    }
}