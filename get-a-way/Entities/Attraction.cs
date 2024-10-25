using System.Xml.Serialization;

namespace get_a_way.Entities;

[Serializable]
[XmlInclude(typeof(Historical))]
[XmlInclude(typeof(Active))]
[XmlInclude(typeof(NightLife))]
    
public abstract class Attraction : Place
{
    public int EntryFee { get; set; }
    public int AgeRestrictions { get; set; }
    public List<string> Events { get; set; }

    public Attraction() : this(0, 0, new List<string>())
    {
    }

    protected Attraction(int entryFee, int ageRestrictions, List<string> events)
    {
        EntryFee = entryFee;
        AgeRestrictions = ageRestrictions;
        Events = events;
    }
}