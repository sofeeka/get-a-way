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

    public Shop(ShopType type, bool onlineOrderAvailability)
    {
        Type = type;
        OnlineOrderAvailability = onlineOrderAvailability;
        HolidaySpecials = new List<string>();
    }
}