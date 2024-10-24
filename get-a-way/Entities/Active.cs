namespace get_a_way.Entities;
[Serializable]
public class Active : AbstractEvent
{
    public string activityType { get;   set; }

    public Active()
    {
    }

    public Active(string activityType)
    {
        this.activityType = activityType;
    }
}