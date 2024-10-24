namespace get_a_way.Entities;

[Serializable]
public class Active : Attraction
{
    public string ActivityType { get; set; }

    public Active()
    {
    }

    public Active(string activityType)
    {
        this.ActivityType = activityType;
    }
}