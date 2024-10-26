namespace get_a_way.Entities.Places.Attractions;

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