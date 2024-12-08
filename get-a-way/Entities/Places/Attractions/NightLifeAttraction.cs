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

    public NightLifeAttraction(string dressCode)
    {
        DressCode = dressCode;
    }

    private string ValidateDressCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("DressCode cannot be empty");

        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Dress Code: {DressCode}\n";
    }
}