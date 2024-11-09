using System.Xml.Serialization;
using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Attractions;

[Serializable]
[XmlInclude(typeof(HistoricalAttraction))]
[XmlInclude(typeof(ActiveAttraction))]
[XmlInclude(typeof(NightLifeAttraction))]
public abstract class Attraction : Place
{
    private int _entryFee;
    private int _minimalAge;
    private List<string> _events;
    private string _description;

    public int EntryFee
    {
        get => _entryFee;
        set => _entryFee = ValidateEntryFee(value);
    }

    public int MinimalAge
    {
        get => _minimalAge;
        set => _minimalAge = ValidateMinimalAge(value);
    }

    public List<string> Events
    {
        get => _events;
        set => _events = ValidateEvents(value);
    }

    public string Description
    {
        get => _description;
        set => _description = ValidateDescription(value);
    }

    public Attraction()
    {
    }

    protected Attraction(string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory,
        bool petFriendly, int entryFee, int minimalAge, string description)
        : base(name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        EntryFee = entryFee;
        MinimalAge = minimalAge;
        Description = description;
        Events = new List<string>();
    }

    private int ValidateEntryFee(int value)
    {
        if (value < 0)
            throw new InvalidAttributeException("Entry fee can not be negative value.");
        return value;
    }

    private int ValidateMinimalAge(int value)
    {
        if (value is < 0 or > 120)
            throw new InvalidAttributeException("Minimal age should be between 0 and 120.");
        return value;
    }

    private List<string> ValidateEvents(List<string> values)
    {
        if (values.Equals(null))
            throw new InvalidAttributeException("Event list can not be null.");
        foreach (var value in values)
            if (string.IsNullOrEmpty(value))
                throw new InvalidAttributeException("Event cannot be null.");
        return values;
    }

    private string ValidateDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Description can not be empty.");
        if (value.Length is < 10 or > 100)
            throw new InvalidAttributeException("Description length must be between 10 and 1000 characters.");
        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Entry Fee: {EntryFee}\n" +
               $"Minimal Age: {MinimalAge}\n" +
               $"Events: {GetEvents()}\n" +
               $"Description: {Description}\n";
    }

    private string GetEvents()
    {
        if (Events.Count == 0)
            return "No events available";
        return string.Join(", ", Events);
    }
}