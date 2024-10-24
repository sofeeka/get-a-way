namespace get_a_way.Entities;

[Serializable]
public class Historical : Attraction
{
    public string CulturalPeriod { get; set; }

    public Historical()
    {
    }

    public Historical(string culturalPeriod)
    {
        this.CulturalPeriod = culturalPeriod;
    }
}