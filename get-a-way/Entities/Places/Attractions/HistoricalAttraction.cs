namespace get_a_way.Entities.Places.Attractions;

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