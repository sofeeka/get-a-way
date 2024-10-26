namespace get_a_way.Entities;

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