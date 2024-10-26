namespace get_a_way.Entities.Places.Attractions;

[Serializable]
public class ActiveAttraction : Attraction
{
    public string ActivityType { get; set; }

    public ActiveAttraction()
    {
    }

    public ActiveAttraction(string activityType)
    {
        ActivityType = activityType;
    }
}