namespace get_a_way.Entities.Places.Shop;

[Serializable]
public class Shop : Place
{
    public ShopType Type { get; set; }
    public bool OnlineOrderAvailability { get; set; }
    public List<string> HolidaySpecials { get; set; }

    public Shop()
    {
    }

    public Shop(string name, string location, DateTime openTime, DateTime closeTime, PriceCategory priceCategory, 
        bool petFriendly, ShopType type, bool onlineOrderAvailability) 
        : base( name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        Type = type;
        OnlineOrderAvailability = onlineOrderAvailability;
        HolidaySpecials = new List<string>();
    }
}