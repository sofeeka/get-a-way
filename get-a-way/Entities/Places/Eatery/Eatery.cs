namespace get_a_way.Entities.Places.Eatery;

[Serializable]
public class Eatery : Place
{
    public EateryType Type { get; set; }
    public Cuisine Cuisine { get; set; }
    public List<string> Menu { get; set; }
    public HashSet<DietaryOptions> DietaryOptions { get; set; }
    public bool ReservationRequired { get; set; }

    public Eatery()
    {
    }

    public Eatery(string name, string location, DateTime openTime, DateTime closeTime, PriceCategory priceCategory, 
        bool petFriendly, EateryType type, Cuisine cuisine, bool reservationRequired)
        : base( name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        Type = type;
        Cuisine = cuisine;
        Menu = new List<string>();
        DietaryOptions = new HashSet<DietaryOptions>();
        ReservationRequired = reservationRequired;
    }
}