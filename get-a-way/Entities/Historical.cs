namespace get_a_way.Entities;

[Serializable]
public class Historical : AbstractEvent
{
    public string culturalPeriod { get;   set; }

    public Historical()
    {
    }

    public Historical(string culturalPeriod)
    {
        this.culturalPeriod = culturalPeriod;
    }
}