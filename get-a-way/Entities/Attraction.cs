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
    public List<AbstractEvent> Events { get; set; }

    public Attraction() : this(0, 0, new List<AbstractEvent>())
    {
    }

    protected Attraction(int entryFee, int ageRestrictions, List<AbstractEvent> events)
    {
        this.EntryFee = entryFee;
        this.AgeRestrictions = ageRestrictions;
        this.Events = events;
    }
}