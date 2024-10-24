namespace get_a_way.Entities;

[Serializable]
public class Shop
{
    public bool onlineOrderAvailability { get;   set; }

    public List<string> holidaySpecials { get;   set; }

    public Shop()
    {
    }

    public Shop(bool onlineOrderAvailability, List<string> holidaySpecials)
    {
        this.onlineOrderAvailability = onlineOrderAvailability;
        this.holidaySpecials = holidaySpecials;
    }
}