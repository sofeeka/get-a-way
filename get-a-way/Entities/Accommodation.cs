namespace get_a_way.Entities;

[Serializable]
public class Accommodation
{
    public List<string> amenities { get; private set; }
    public bool parkingAvailable { get; private set; }
    public int maxPeople { get; private set; }

    public BedType bedType { get; private set; }

    public Accommodation()
    {
    }

    public Accommodation(List<string> amenities = null, bool parkingAvailable = false, int maxPeople = 0, BedType bedType = BedType.Single)
    {
        this.amenities = amenities;
        this.parkingAvailable = parkingAvailable;
        this.maxPeople = maxPeople;
        this.bedType = bedType;
    }
}