namespace get_a_way.Entities.Places;

[Serializable]
public class LocalPlace : Place
{
    public LocalPlace()
    {
    }

    public LocalPlace(string name, string location, DateTime openTime, DateTime closeTime, PriceCategory priceCategory,
        bool petFriendly) : base(name, location, openTime, closeTime, priceCategory, petFriendly)
    {
    }
}