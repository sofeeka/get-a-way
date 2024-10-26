namespace get_a_way.Entities;

[Serializable]
public class HistoricalAttraction : Attraction
{
    public string CulturalPeriod { get; set; }

    public HistoricalAttraction()
    {
    }

    public HistoricalAttraction(string culturalPeriod)
    {
        CulturalPeriod = culturalPeriod;
    }
}