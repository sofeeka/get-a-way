namespace get_a_way.Entities;

[Serializable]
public class Local : Place
{
    public Local()
    {
    }

    public Local(long id, string name, string location, string type, DateTime openTime, DateTime closeTime, string priceCategory,
        bool petFriendly, bool nightAttraction, List<Review> reviews) : base(id, name, location, type, openTime,
        closeTime, priceCategory, petFriendly, nightAttraction, reviews)
    {
    }
}