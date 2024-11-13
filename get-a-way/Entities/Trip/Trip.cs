﻿using System.Text.RegularExpressions;
using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Trip;

[Serializable]
public class Trip : IExtent<Trip>
{
    private static List<Trip> _extent = new List<Trip>();

    private static long IdCounter = 0;
    private long _id;
    private Account _account;
    private DateTime _date;
    private TripType _tripType;
    private List<String> _pictures;
    private String _description;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public Account Account
    {
        get => _account;
        set => _account = value;
    }

    public DateTime Date
    {
        get => _date;
        set => _date = ValidateDate(value);
    }

    public TripType TripType
    {
        get => _tripType;
        set => _tripType = value;
    }

    public List<String> Pictures
    {
        get => _pictures;
        set => _pictures = ValidatePictures(value);
    }

    public String Description
    {
        get => _description;
        set => _description = ValidateDescription(value);
    }

    public Trip()
    {
    }

    public Trip(Account account, DateTime date, TripType tripType, string description)
    {
        ID = ++IdCounter;
        Account = account;
        Date = date;
        TripType = tripType;
        Pictures = new List<string>();
        Description = description;

        AddInstanceToExtent(this);
    }

    private DateTime ValidateDate(DateTime date)
    {
        if (date > DateTime.Now)
            throw new InvalidAttributeException("Date cannot be in the future.");
        return date;
    }

    private List<string> ValidatePictures(List<string> pictures)
    {
        if (pictures == null)
            throw new InvalidAttributeException("Pictures list cannot be null.");

        if (pictures.Count > 10)
            throw new InvalidAttributeException("Pictures list cannot contain more than 10 images.");

        foreach (var picture in pictures)
        {
            if (string.IsNullOrWhiteSpace(picture) || !IsValidImageUrl(picture))
                throw new InvalidAttributeException($"Invalid picture URL: '{picture}'.");
        }

        return pictures;
    }

    private bool IsValidImageUrl(string url)
    {
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
    }

    private string ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidAttributeException("Description cannot be empty.");
        if (description.Length > 1000)
            throw new InvalidAttributeException("Description cannot exceed 1000 characters.");
        return description;
    }

    public static List<Trip> GetExtentCopy()
    {
        return new List<Trip>(_extent);
    }

    public static void AddInstanceToExtent(Trip instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add((instance));
    }

    public static void RemoveInstanceFromExtent(Trip instance)
    {
        _extent.Remove(instance);
    }

    public static List<Trip> GetExtent()
    {
        return _extent;
    }

    public static void ResetExtent()
    {
        _extent.Clear();
        IdCounter = 0;
    }

    public override string ToString()
    {
        return $"Trip ID: {ID}\n" +
               $"Account: {Account.Username}\n" +
               $"Date: {Date:yyyy-MM-dd}\n" +
               $"Trip Type: {TripType}\n" +
               $"Pictures: {GetPictures()}\n" +
               $"Description: {(string.IsNullOrWhiteSpace(Description) ? "No description provided" : Description)}\n";
    }

    private string GetPictures()
    {
        if (Pictures.Count == 0)
            return "No pictures available";
        return string.Join(", ", Pictures);
    }
}