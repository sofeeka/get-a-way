namespace get_a_way.Entities;

[Serializable]
public class NightLifeAttraction : Attraction
{
    public string DressCode { get; set; }

    public NightLifeAttraction()
    {
    }

    public NightLifeAttraction(string dressCode)
    {
        DressCode = dressCode;
    }
}