﻿using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Attractions;

[Serializable]
public class ActiveAttraction : Attraction
{
    private string _activityType;

    public string ActivityType
    {
        get => _activityType;
        set => _activityType = ValidateActivityType(value);
    }

    public ActiveAttraction()
    {
    }

    public ActiveAttraction(HashSet<OwnerAccount> owners, string name, string location, DateTime openTime,
        DateTime closeTime, PriceCategory priceCategory, bool petFriendly, int entryFee, int minimalAge,
        string description, string activityType, bool isDummy = false) : base(owners, name, location, openTime,
        closeTime, priceCategory, petFriendly, entryFee, minimalAge, description, isDummy)
    {
        ActivityType = activityType;
    }

    private string ValidateActivityType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Activity type of active attraction cannot be empty");

        if (value.Length is < 5 or > 1000)
            throw new InvalidAttributeException("Activity type length must be between 5 and 1000 characters.");

        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Activity Type: {ActivityType}\n";
    }
}