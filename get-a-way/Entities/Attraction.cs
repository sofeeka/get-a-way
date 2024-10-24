using System.Xml.Serialization;

namespace get_a_way.Entities;
[Serializable]
[XmlInclude(typeof(Historical))]
[XmlInclude(typeof(Active))]
[XmlInclude(typeof(NightLife))]
public abstract class Attraction
{
    public int entryFee { get;   set; }

    public int ageRestrictions { get;   set; }

    public List<AbstractEvent> events { get;   set; }
    
    public Attraction() : this(entryFee:0, ageRestrictions:0, events: new List<AbstractEvent>()) { }
    protected Attraction(int entryFee, int ageRestrictions, List<AbstractEvent> events)
    {
        this.entryFee = entryFee;
        this.ageRestrictions = ageRestrictions;
        this.events = events;
    }
}