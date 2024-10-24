namespace get_a_way.Entities;

public abstract class Place
{
    public long ID { get; private set; }
    public string name { get; private set; }
    public string location { get; private set; }
    public DateTime openTime { get; private set; }
    public DateTime closeTime { get; private set; }
    public string priceCategory { get; private set; }
    public bool petFriendly { get; private set; }
    public bool nightAttraction { get; private set; }
    public PlaceType placeType { get; private set; }
    public List<Review> reviews { get; private set; }

    public Accommodation accommodation { get; private set; }
    public Eateries eateries { get; private set; }
    public List<Attractions> attractions { get; private set; }
    public List<Shop> shops { get; private set; }

    protected Place(long id, string name, string location, DateTime openTime, DateTime closeTime, string priceCategory,
        bool petFriendly, bool nightAttraction, PlaceType placeType, List<Review> reviews, Accommodation accommodation,
        Eateries eateries, List<Attractions> attractions, List<Shop> shops)
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