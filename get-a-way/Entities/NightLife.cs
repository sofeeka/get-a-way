namespace get_a_way.Entities;

[Serializable]
public class NightLife : Attraction
{
    public string dressCode { get; set; }

    public NightLife()
    {
    }

    public NightLife(string dressCode)
    {
        this.dressCode = dressCode;
    }
}