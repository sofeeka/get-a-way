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

    public HistoricalAttraction(string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly, int entryFee, int minimalAge, string description,
        string culturalPeriod) : base(name, location, openTime, closeTime, priceCategory, petFriendly, entryFee,
        minimalAge, description)
    {
        CulturalPeriod = culturalPeriod;
    }

    private string ValidateCulturalPeriod(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Cultural period of historical attraction cannot be empty");

        if (value.Length is < 5 or > 1000)
            throw new InvalidAttributeException("Cultural period length must be between 5 and 1000 characters.");

        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Cultural Period: {CulturalPeriod}\n";
    }
}