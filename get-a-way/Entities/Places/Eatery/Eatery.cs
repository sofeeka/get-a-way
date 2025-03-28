﻿using get_a_way.Entities.Accounts;
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
        set => _dietaryOptions = ValidateDietaryOptions(value);
    }

    public bool ReservationRequired
    {
        get => _reservationRequired;
        set => _reservationRequired = value;
    }

    public Eatery()
    {
    }

    public Eatery(HashSet<OwnerAccount> owners, string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly, EateryType type, Cuisine cuisine, bool reservationRequired,
        bool isDummy = false) : base(owners, name, location, openTime, closeTime, priceCategory, petFriendly, isDummy)
    {
        Type = type;
        Cuisine = cuisine;
        Menu = new List<string>();
        DietaryOptions = new HashSet<DietaryOptions>();
        ReservationRequired = reservationRequired;
    }

    private List<string> ValidateMenu(List<string> values)
    {
        if (values == null)
            throw new InvalidAttributeException("Menu cannot be null.");

        foreach (var value in values)
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidMenuItemException("Menu items cannot be empty");

        return values;
    }

    private HashSet<DietaryOptions> ValidateDietaryOptions(HashSet<DietaryOptions> value)
    {
        if (value == null)
            throw new InvalidAttributeException("Dietary Options cannot be null");
        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Eatery Type: {Type}\n" +
               $"Cuisine: {Cuisine}\n" +
               $"Menu: {GetMenuItems()}\n" +
               $"Dietary Options: {GetDietaryOptions()}\n" +
               $"Reservation Required: {(ReservationRequired ? "Yes" : "No")}\n";
    }

    private string GetMenuItems()
    {
        if (Menu.Count == 0)
            return "No menu available";
        return string.Join(", ", Menu);
    }

    private string GetDietaryOptions()
    {
        if (DietaryOptions.Count == 0)
            return "None";
        return string.Join(", ", DietaryOptions);
    }
}