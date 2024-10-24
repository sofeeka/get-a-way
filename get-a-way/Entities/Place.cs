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
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public string PriceCategory { get; set; }
    public bool PetFriendly { get; set; }
    public bool NightAttraction { get; set; }
    public List<Review> Reviews { get; set; }
    public Accommodation Accommodation { get; set; }
    public Eatery Eatery { get; set; }
    public List<Attraction> Attractions { get; set; }
    public List<Shop> Shops { get; set; }

    public Place()
    {
    }

    protected Place(long id, string name, string location, DateTime openTime, DateTime closeTime, string priceCategory,
        bool petFriendly, bool nightAttraction, List<Review> reviews, Accommodation accommodation,
        Eatery eatery, List<Attraction> attractions, List<Shop> shops)
    {
        ID = id;
        this.Name = name;
        this.Location = location;
        this.OpenTime = openTime;
        this.CloseTime = closeTime;
        this.PriceCategory = priceCategory;
        this.PetFriendly = petFriendly;
        this.NightAttraction = nightAttraction;
        this.Reviews = reviews;
        this.Accommodation = accommodation;
        this.Eatery = eatery;
        this.Attractions = attractions;
        this.Shops = shops;
    }
}