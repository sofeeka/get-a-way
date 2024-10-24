namespace get_a_way.Entities;

public abstract class Attractions
{
    public int entryFee { get; private set; }

    public int ageRestrictions { get; private set; }

    public List<AbstractEvent> events { get; private set; }

    protected Attractions(int entryFee, int ageRestrictions, List<AbstractEvent> events)
    {
        this.entryFee = entryFee;
        this.ageRestrictions = ageRestrictions;
        this.events = events;
    }
}