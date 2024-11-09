using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Shop;

[Serializable]
public class Shop : Place
{
    private ShopType _type;
    private bool _onlineOrderAvailability;
    private List<string> _holidaySpecials;

    public ShopType Type
    {
        get => _type;
        set => _type = value;
    }

    public bool OnlineOrderAvailability
    {
        get => _onlineOrderAvailability;
        set => _onlineOrderAvailability = value;
    }

    public List<string> HolidaySpecials
    {
        get => _holidaySpecials;
        set => _holidaySpecials = ValidateHolidaySpecials(value);
    }

    public Shop()
    {
    }

    public Shop(string name, string location, DateTime openTime, DateTime closeTime, PriceCategory priceCategory,
        bool petFriendly, ShopType type, bool onlineOrderAvailability)
        : base(name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        Type = type;
        OnlineOrderAvailability = onlineOrderAvailability;
        HolidaySpecials = new List<string>();
    }

    private List<string> ValidateHolidaySpecials(List<string> values)
    {
        foreach (var value in values)
            if (string.IsNullOrEmpty(value))
                throw new InvalidAttributeException("Holiday specials items cannot be empty");

        return values;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Shop Type: {Type}\n" +
               $"Online Order Available: {(OnlineOrderAvailability ? "Yes" : "No")}\n" +
               $"Holiday Specials: {GetHolidaySpecials()}\n";
    }

    private string GetHolidaySpecials()
    {
        if (HolidaySpecials.Count == 0)
            return "None";
        return string.Join(", ", HolidaySpecials);
    }
}