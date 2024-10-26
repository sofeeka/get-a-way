using System.Xml.Serialization;

namespace get_a_way.Entities;

[Serializable]
[XmlInclude(typeof(HistoricalAttraction))]
[XmlInclude(typeof(ActiveAttraction))]
[XmlInclude(typeof(NightLifeAttraction))]
    
public abstract class Attraction : Place
{
    public int EntryFee { get; set; }
    public int AgeRestrictions { get; set; }
    public List<string> Events { get; set; }
    public string Description { get; set; }

    public Attraction() : this(0, 0, "default")
    {
    }

    protected Attraction(int entryFee, int ageRestrictions, string description)
    {
        EntryFee = entryFee;
        AgeRestrictions = ageRestrictions;
        Description = description;
        Events = new List<string>();
    }
}