namespace get_a_way.Entities;

public class Active : AbstractEvent
{
    public string activityType { get; private set; }

    public Active(string activityType)
    {
        this.activityType = activityType;
    }
}