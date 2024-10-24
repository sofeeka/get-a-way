namespace get_a_way.Entities;

[Serializable]
public class Accommodation : Place
{
    public List<string> Amenities { get; set; }
    public bool ParkingAvailable { get; set; }
    public int MaxPeople { get; set; }

    public BedType BedType { get; set; }

    public Accommodation() 
    {
    }

    // TODO initialise class Place fields
    public Accommodation(List<string> amenities = null, bool parkingAvailable = false, int maxPeople = 0,
        BedType bedType = BedType.Single)
    {
        this.Amenities = amenities;
        this.ParkingAvailable = parkingAvailable;
        this.MaxPeople = maxPeople;
        this.BedType = bedType;
    }
}