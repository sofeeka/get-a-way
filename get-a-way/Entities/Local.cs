namespace get_a_way.Entities;

public class Local : Place
{
    public Local(long id, string name, string location, DateTime openTime, DateTime closeTime, string priceCategory,
        bool petFriendly, bool nightAttraction, PlaceType placeType, List<Review> reviews, Accommodation accommodation,
        Eateries eateries, List<Attractions> attractions, List<Shop> shops) : base(id, name, location, openTime,
        closeTime, priceCategory, petFriendly, nightAttraction, placeType, reviews, accommodation, eateries,
        attractions, shops)
    {
    }
}