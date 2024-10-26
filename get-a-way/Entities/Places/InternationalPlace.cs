namespace get_a_way.Entities.Places;

[Serializable]
public class InternationalPlace : Place
{
    public InternationalPlace()
    {
    }

    public InternationalPlace(long id, string name, string location, DateTime openTime, DateTime closeTime,
        string priceCategory, bool petFriendly, bool nightAttraction) : base(id,
        name, location, openTime, closeTime, priceCategory, petFriendly, nightAttraction)
    {
    }
}