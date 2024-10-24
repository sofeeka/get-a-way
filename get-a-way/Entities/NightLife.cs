namespace get_a_way.Entities;

[Serializable]
public class NightLife : Attraction
{
    public string DressCode { get; set; }

    public NightLife()
    {
    }

    public NightLife(string dressCode)
    {
        this.DressCode = dressCode;
    }
}