namespace get_a_way.Entities;

public class Historical : AbstractEvent
{
    public string culturalPeriod { get; private set; }

    public Historical(string culturalPeriod)
    {
        this.culturalPeriod = culturalPeriod;
    }
}