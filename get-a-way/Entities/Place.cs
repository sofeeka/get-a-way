using System.Xml.Serialization;

namespace get_a_way.Entities;

[Serializable]
[XmlInclude(typeof(Accommodation))]
[XmlInclude(typeof(Eateries))]
[XmlInclude(typeof(Attraction))]
[XmlInclude(typeof(Shop))]
public abstract class Place
{
    public static List<Place> Extent = new List<Place>();

    public long ID { get;   set; }
    public string name { get;   set; }
    public string location { get;   set; }
    public DateTime openTime { get;   set; }
    public DateTime closeTime { get;   set; }
    public string priceCategory { get;   set; }
    public bool petFriendly { get;   set; }
    public bool nightAttraction { get;   set; }
    public PlaceType placeType { get;   set; }
    public List<Review> reviews { get;   set; }

    public Accommodation accommodation { get;   set; }
    public Eateries eateries { get;   set; }
    public List<Attraction> attractions { get;   set; }
    public List<Shop> shops { get;   set; }

    public Place()
    { }

    protected Place(long id, string name, string location, DateTime openTime, DateTime closeTime, string priceCategory,
        bool petFriendly, bool nightAttraction, PlaceType placeType, List<Review> reviews, Accommodation accommodation,
        Eateries eateries, List<Attraction> attractions, List<Shop> shops)
    {
        ID = id;
        this.name = name;
        this.location = location;
        this.openTime = openTime;
        this.closeTime = closeTime;
        this.priceCategory = priceCategory;
        this.petFriendly = petFriendly;
        this.nightAttraction = nightAttraction;
        this.placeType = placeType;
        this.reviews = reviews;
        this.accommodation = accommodation;
        this.eateries = eateries;
        this.attractions = attractions;
        this.shops = shops;
    }
}