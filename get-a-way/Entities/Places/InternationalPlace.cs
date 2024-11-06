namespace get_a_way.Entities.Places;

[Serializable]
public class InternationalPlace : Place
{
    public InternationalPlace()
    {
    }

    public InternationalPlace(string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly) 
        : base(name, location, openTime, closeTime, priceCategory, petFriendly)
    {
    }
}