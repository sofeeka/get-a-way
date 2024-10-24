namespace get_a_way.Entities;

public class Accommodation
{
    public List<string> amenities { get; private set; }
    public bool parkingAvailable { get; private set; }
    public int maxPeople { get; private set; }

    public BedType bedType { get; private set; }

    public Accommodation(List<string> amenities, bool parkingAvailable, int maxPeople, BedType bedType)
    {
        this.amenities = amenities;
        this.parkingAvailable = parkingAvailable;
        this.maxPeople = maxPeople;
        this.bedType = bedType;
    }
}