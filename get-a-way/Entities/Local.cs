namespace get_a_way.Entities;

[Serializable]
public class Local : Place
{
    public Local()
    {
    }

    public Local(long id, string name, string location, DateTime openTime, DateTime closeTime, string priceCategory,
        bool petFriendly, bool nightAttraction, List<Review> reviews, Accommodation accommodation,
        Eatery eatery, List<Attraction> attractions, List<Shop> shops) : base(id, name, location, openTime,
        closeTime, priceCategory, petFriendly, nightAttraction, reviews, accommodation, eatery,
        attractions, shops)
    {
    }
}