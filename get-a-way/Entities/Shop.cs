namespace get_a_way.Entities;

public class Shop
{
    public bool onlineOrderAvailability { get; private set; }

    public List<string> holidaySpecials { get; private set; }

    public Shop(bool onlineOrderAvailability, List<string> holidaySpecials)
    {
        this.onlineOrderAvailability = onlineOrderAvailability;
        this.holidaySpecials = holidaySpecials;
    }
}