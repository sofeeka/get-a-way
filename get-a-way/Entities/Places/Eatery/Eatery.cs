using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Eatery;

[Serializable]
public class Eatery : Place
{
    private EateryType _type;
    private Cuisine _cuisine;
    private List<string> _menu;
    private HashSet<DietaryOptions> _dietaryOptions;
    private bool _reservationRequired;

    public EateryType Type
    {
        get => _type;
        set => _type = value;
    }

    public Cuisine Cuisine
    {
        get => _cuisine;
        set => _cuisine = value;
    }

    public List<string> Menu
    {
        get => _menu;
        set => _menu = ValidateMenu(value);
    }

    public HashSet<DietaryOptions> DietaryOptions
    {
        get => _dietaryOptions;
        set => _dietaryOptions = value;
    }

    public bool ReservationRequired
    {
        get => _reservationRequired;
        set => _reservationRequired = value;
    }

    public Eatery()
    {
    }

    public Eatery(string name, string location, DateTime openTime, DateTime closeTime, PriceCategory priceCategory,
        bool petFriendly, EateryType type, Cuisine cuisine, bool reservationRequired)
        : base(name, location, openTime, closeTime, priceCategory, petFriendly)
    {
        Type = type;
        Cuisine = cuisine;
        Menu = new List<string>();
        DietaryOptions = new HashSet<DietaryOptions>();
        ReservationRequired = reservationRequired;
    }

    private List<string> ValidateMenu(List<string> values)
    {
        foreach (var value in values)
            if (string.IsNullOrEmpty(value))
                throw new InvalidAttributeException("Menu items cannot be empty");

        return values;
    }
}