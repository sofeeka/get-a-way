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
    public static List<Place> Extent = new List<Place>();

    private static long IdCounter = 0;
    public long ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public PriceCategory PriceCategory { get; set; }
    public bool PetFriendly { get; set; }
    public bool OpenedAtNight { get; set; }
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
        TimeSpan nightStart = new TimeSpan(20, 0, 0); 
        TimeSpan nightEnd = new TimeSpan(6, 0, 0);    

        TimeSpan openTime = OpenTime.TimeOfDay;
        TimeSpan closeTime = CloseTime.TimeOfDay;

        bool opensDuringNight = (openTime <= nightEnd || openTime >= nightStart);
        bool closesDuringNight = (closeTime >= nightStart || closeTime <= nightEnd);

        OpenedAtNight = opensDuringNight || closesDuringNight;
    }

    public List<Place> GetExtentCopy()
    {
        return new List<Place>(Extent);
    }

    public void AddInstanceToExtent(Place instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(Place instance)
    {
        Extent.Remove(instance);
    }
}