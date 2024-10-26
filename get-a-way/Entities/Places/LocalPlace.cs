namespace get_a_way.Entities.Places;

[Serializable]
public class LocalPlace : Place
{
    public LocalPlace()
    {
    }

    public LocalPlace(long id, string name, string location, DateTime openTime, DateTime closeTime, string priceCategory,
        bool petFriendly, bool nightAttraction) : base(id, name, location, openTime,
        closeTime, priceCategory, petFriendly, nightAttraction)
    {
    }
}