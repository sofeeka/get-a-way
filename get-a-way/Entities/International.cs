namespace get_a_way.Entities;

[Serializable]
public class International : Place
{
    public International()
    {
    }

    public International(long id, string name, string location, string type, DateTime openTime, DateTime closeTime,
        string priceCategory, bool petFriendly, bool nightAttraction, List<Review> reviews,
        Accommodation accommodation, Eatery eatery, List<Attraction> attractions, List<Shop> shops) : base(id,
        name, location, type, openTime, closeTime, priceCategory, petFriendly, nightAttraction, reviews)
    {
    }
}