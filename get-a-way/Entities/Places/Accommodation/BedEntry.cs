using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Accommodation;

[Serializable]
public class BedEntry
{
    private BedType _bedType;
    private int _count;

    public BedType BedType
    {
        get => _bedType;
        set => _bedType = value;
    }

    public int Count
    {
        get => _count;
        set => _count = ValidateCount(value);
    }

    private int ValidateCount(int value)
    {
        if (value <= 0)
            throw new InvalidAttributeException("Bed count cannot be negative");

        return value;
    }
}