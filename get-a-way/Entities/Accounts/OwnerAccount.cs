﻿using System.Xml.Serialization;
using get_a_way.Exceptions;

namespace get_a_way.Entities.Accounts;

[Serializable]
public class OwnerAccount : Account
{
    private static double _tax = 15;

    public static double Tax
    {
        get => _tax;
        set => _tax = ValidateTax(value);
    }

    private static double ValidateTax(double value)
    {
        if (value is < 0.0 or > 100.0)
            throw new InvalidAttributeException($"Invalid tax '{value}'. Tax has to be in between 0.0 % and 100.0 %");

        return value;
    }

    public OwnerAccount()
    {
    }

    public OwnerAccount(string username, string password, string email) :
        base(username, password, email)
    {
    }
}