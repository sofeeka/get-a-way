﻿using System.Text.RegularExpressions;
using System.Xml.Serialization;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Places;

[Serializable]
[XmlInclude(typeof(Accommodation.Accommodation))]
[XmlInclude(typeof(Eatery.Eatery))]
[XmlInclude(typeof(Attractions.Attraction))]
[XmlInclude(typeof(Shop.Shop))]
public abstract class Place : IExtent<Place>
{
    private static List<Place> _extent = new List<Place>();

    private static long IdCounter = 0;

    private long _id;
    private string _name;
    private string _location;
    private List<String> _pictureUrls; // todo addPicture()
    private DateTime _openTime;
    private DateTime _closeTime;
    private PriceCategory _priceCategory;
    private bool _petFriendly;
    private bool _openedAtNight;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = ValidateName(value);
    }

    public string Location
    {
        get => _location;
        set => _location = ValidateLocation(value);
    }

    public List<String> PictureUrls
    {
        get => _pictureUrls;
        set => _pictureUrls = ValidatePictureUrls(value);
    }

    public DateTime OpenTime
    {
        get => _openTime;
        set
        {
            _openTime = value;
            SetOpenedAtNight(); // recalculate when OpenTime is updated
        }
    }

    public DateTime CloseTime
    {
        get => _closeTime;
        set
        {
            _closeTime = value;
            SetOpenedAtNight(); // recalculate when CloseTime is updated
        }
    }

    public PriceCategory PriceCategory
    {
        get => _priceCategory;
        set => _priceCategory = value;
    }

    public bool PetFriendly
    {
        get => _petFriendly;
        set => _petFriendly = value;
    }

    public bool OpenedAtNight
    {
        get => _openedAtNight;
        set => _openedAtNight = value;
    }

    [XmlArray("Reviews")]
    [XmlArrayItem("Review")]
    public List<Review.Review> Reviews { get; set; }

    public Place()
    {
    }

    protected Place(string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly)
    {
        ID = ++IdCounter;
        Name = name;
        Location = location;
        OpenTime = openTime;
        CloseTime = closeTime;
        PriceCategory = priceCategory;
        PetFriendly = petFriendly;
        Reviews = new List<Review.Review>();
        SetOpenedAtNight();

        AddInstanceToExtent(this);
    }

    private void SetOpenedAtNight()
    {
        TimeSpan nightStart = new TimeSpan(22, 0, 0);
        TimeSpan nightEnd = new TimeSpan(6, 0, 0);

        TimeSpan openTime = OpenTime.TimeOfDay;
        TimeSpan closeTime = CloseTime.TimeOfDay;

        bool opensDuringNight = (openTime < nightEnd || openTime > nightStart);
        bool closesDuringNight = (closeTime > nightStart || closeTime < nightEnd);

        OpenedAtNight = opensDuringNight || closesDuringNight;
    }

    private string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 40)
            throw new InvalidAttributeException("Name must be at least 3 characters long.");
        return name;
    }

    //todo validate through API
    private string ValidateLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new InvalidAttributeException("Location must not be null or white space");
        return location;
    }

    private List<string> ValidatePictureUrls(List<string> urls)
    {
        if (urls == null)
            throw new InvalidAttributeException("Pictures list cannot be null.");

        if (urls.Count > 10)
            throw new InvalidAttributeException("Pictures list cannot contain more than 10 images.");

        if (urls.Any(url => string.IsNullOrWhiteSpace(url) || !IsValidImageUrl(url)))
        {
            throw new InvalidPictureUrlException();
        }

        return urls;
    }

    private bool IsValidImageUrl(string url)
    {
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
    }

    public static List<Place> GetExtentCopy()
    {
        return new List<Place>(_extent);
    }

    public static void AddInstanceToExtent(Place instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add((instance));
    }

    public static void RemoveInstanceFromExtent(Place instance)
    {
        _extent.Remove(instance);
    }

    public static List<Place> GetExtent()
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
        return $"Place Details:\n" +
               $"ID: {ID}\n" +
               $"Name: {Name}\n" +
               $"Location: {Location}\n" +
               $"Pictures: {GetPictureUrls()}\n" +
               $"Open Time: {OpenTime:HH:mm}\n" +
               $"Close Time: {CloseTime:HH:mm}\n" +
               $"Price Category: {PriceCategory}\n" +
               $"Pet Friendly: {(PetFriendly ? "Yes" : "No")}\n" +
               $"Opened At Night: {(OpenedAtNight ? "Yes" : "No")}\n" +
               $"Number of Reviews: {Reviews?.Count ?? 0}\n";
    }

    private string GetPictureUrls()
    {
        if (PictureUrls == null || PictureUrls.Count == 0)
            return "No pictures available";
        return string.Join(", ", PictureUrls);
    }
}