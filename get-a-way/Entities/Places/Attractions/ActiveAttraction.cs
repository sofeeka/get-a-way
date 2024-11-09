using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Attractions;

[Serializable]
public class ActiveAttraction : Attraction
{
    private string _activityType;

    public string ActivityType
    {
        get => _activityType;
        set => _activityType = ValidateActivityType(value);
    }

    public ActiveAttraction()
    {
    }

    public ActiveAttraction(string activityType)
    {
        ActivityType = activityType;
    }

    private string ValidateActivityType(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidAttributeException("Activity type of active attraction cannot be empty");

        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Activity Type: {ActivityType}\n";
    }
}