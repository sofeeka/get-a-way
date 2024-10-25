using System.Xml.Serialization;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities;

[Serializable]
[XmlInclude(typeof(Accommodation))]
[XmlInclude(typeof(Eatery))]
[XmlInclude(typeof(Attraction))]
[XmlInclude(typeof(Shop))]
public abstract class Place : IExtent<Place>
{
    public static List<Place> Extent = new List<Place>();

    public long ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public string PriceCategory { get; set; }
    public bool PetFriendly { get; set; }
    public bool NightAttraction { get; set; }
    public List<Review> Reviews { get; set; }

    public Place()
    {
    }

    protected Place(long id, string name, string location, string type, DateTime openTime, DateTime closeTime,
        string priceCategory,
        bool petFriendly, bool nightAttraction, List<Review> reviews)
    {
        ID = id;
        Name = name;
        Location = location;
        Type = type;
        OpenTime = openTime;
        CloseTime = closeTime;
        PriceCategory = priceCategory;
        PetFriendly = petFriendly;
        NightAttraction = nightAttraction;
        Reviews = reviews;
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