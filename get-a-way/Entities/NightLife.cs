namespace get_a_way.Entities;

public class NightLife : AbstractEvent
{
    public string dressCode { get; private set; }

    public NightLife(string dressCode)
    {
        this.dressCode = dressCode;
    }
}