using System.Xml.Serialization;

namespace get_a_way.Entities.Places.Attractions;

[Serializable]
[XmlInclude(typeof(HistoricalAttraction))]
[XmlInclude(typeof(ActiveAttraction))]
[XmlInclude(typeof(NightLifeAttraction))]
    
public abstract class Attraction : Place
{
    public int EntryFee { get; set; }
    public int MinimalAge { get; set; }
    public List<string> Events { get; set; }
    public string Description { get; set; }

    public Attraction() 
    {
    }

    protected Attraction(string name, string location, DateTime openTime, DateTime closeTime, PriceCategory priceCategory, 
        bool petFriendly, int entryFee, int minimalAge, string description)
        : base( name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        EntryFee = entryFee;
        MinimalAge = minimalAge;
        Description = description;
        Events = new List<string>();
    }
}