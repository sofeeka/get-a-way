using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Attractions;

[Serializable]
public class NightLifeAttraction : Attraction
{
    private string _dressCode;

    public string DressCode
    {
        get => _dressCode;
        set => _dressCode = ValidateDressCode(value);
    }

    public NightLifeAttraction()
    {
    }

    public NightLifeAttraction(string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly, int entryFee, int minimalAge, string description,
        string dressCode) : base(name, location, openTime, closeTime, priceCategory, petFriendly, entryFee,
        minimalAge, description)
    {
        DressCode = dressCode;
    }

    private string ValidateDressCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Dress code cannot be empty");

        if (value.Length is < 5 or > 1000)
            throw new InvalidAttributeException("Dress code length must be between 5 and 1000 characters.");

        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Dress Code: {DressCode}\n";
    }
}