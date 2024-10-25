using System.Xml.Serialization;

namespace get_a_way.Entities;

[Serializable]
[XmlInclude(typeof(Accommodation))]
[XmlInclude(typeof(Eatery))]
[XmlInclude(typeof(Attraction))]
[XmlInclude(typeof(Shop))]
public abstract class Place
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

    protected Place(long id, string name, string location, string type, DateTime openTime, DateTime closeTime, string priceCategory,
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
}