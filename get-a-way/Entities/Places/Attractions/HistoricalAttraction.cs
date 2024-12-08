using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Attractions;

[Serializable]
public class HistoricalAttraction : Attraction
{
    private string _culturalPeriod;

    public string CulturalPeriod
    {
        get => _culturalPeriod;
        set => _culturalPeriod = ValidateCulturalPeriod(value);
    }

    public HistoricalAttraction()
    {
    }

    public HistoricalAttraction(string culturalPeriod)
    {
        CulturalPeriod = culturalPeriod;
    }

    private string ValidateCulturalPeriod(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Cultural period of historical attraction cannot be empty");

        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Cultural Period: {CulturalPeriod}\n";
    }
}