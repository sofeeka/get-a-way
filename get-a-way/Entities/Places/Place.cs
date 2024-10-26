using System.Xml.Serialization;
using get_a_way.Entities.Places.Attractions;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Places;

[Serializable]
[XmlInclude(typeof(Accommodation.Accommodation))]
[XmlInclude(typeof(Eatery.Eatery))]
[XmlInclude(typeof(Attraction))]
[XmlInclude(typeof(Shop.Shop))]
public abstract class Place : IExtent<Place>
{
    public static List<Place> Extent = new List<Place>();

    public long ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public string PriceCategory { get; set; }
    public bool PetFriendly { get; set; }
    public bool NightAttraction { get; set; }  //idk if this needed
    public List<Review.Review> Reviews { get; set; }

    public Place()
    {
    }

    protected Place(long id, string name, string location, DateTime openTime, DateTime closeTime,
        string priceCategory, bool petFriendly, bool nightAttraction)
    {
        ID = id;
        Name = name;
        Location = location;
        OpenTime = openTime;
        CloseTime = closeTime;
        PriceCategory = priceCategory;
        PetFriendly = petFriendly;
        NightAttraction = nightAttraction;
        Reviews = new List<Review.Review>();
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